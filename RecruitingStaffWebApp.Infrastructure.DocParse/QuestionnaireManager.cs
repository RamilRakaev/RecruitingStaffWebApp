using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse
{
    public class QuestionnaireManager : IQuestionnaireManager
    {
        private readonly IRepository<RecruitingStaffWebAppFile> _fileRepository;
        private readonly IRepository<Vacancy> _vacancyRepository;
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<CandidateVacancy> _candidateVacancyRepository;
        private readonly IRepository<Questionnaire> _questionnaireRepository;
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly WebAppOptions _options;

        private RecruitingStaffWebAppFile _file;
        private Vacancy currentVacancy;
        private Candidate currentCandidate;
        private Questionnaire currentQuestionnaire;
        private QuestionCategory currentCategory;

        public QuestionnaireManager(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IRepository<Vacancy> vacancyRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<CandidateVacancy> candidateVacancyRepository,
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<QuestionCategory> questionCategoryRepository,
            IRepository<Question> questionRepository,
            IRepository<Answer> answerRepository,
            IOptions<WebAppOptions> options)
        {
            _fileRepository = fileRepository;
            _vacancyRepository = vacancyRepository;
            _candidateRepository = candidateRepository;
            _candidateVacancyRepository = candidateVacancyRepository;
            _questionnaireRepository = questionnaireRepository;
            _questionCategoryRepository = questionCategoryRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _options = options.Value;
        }

        public string Exception { get; private set; }

        public async Task<bool> ParseAndSaved(string fileName)
        {
            try
            {
                if (_options != null)
                {
                    _file = new RecruitingStaffWebAppFile() { Source = fileName, FileType = FileType.Questionnaire };
                    await _fileRepository.AddAsync(_file);
                    await _fileRepository.SaveAsync();
                    using (var wordDoc = WordprocessingDocument.Open($"{_options.DocumentsSource}\\{_file.Source}", false))
                    {
                        var body = wordDoc.MainDocumentPart.Document.Body;
                        currentQuestionnaire = new Questionnaire()
                        { Name = body.ChildElements.Where(e => e.LocalName == "p").FirstOrDefault().InnerText };

                        foreach (var element in body.ChildElements.Where(e => e.LocalName == "tbl"))
                        {
                            await ParseCandidateWithVacancy(element);
                            foreach (var row in element.ChildElements.Reverse())
                            {
                                foreach (var cell in row.ChildElements)
                                {
                                    var table = cell.FirstOrDefault(c => c.LocalName == "tbl");
                                    if (table != null)
                                    {
                                        await ParseQuestionnaire(table);
                                    }
                                }
                            }
                        }
                    }
                    File.Delete($"{_options.DocumentsSource}\\{_file.Source}");
                }
                return true;
            }
            catch (Exception e)
            {
                Exception = e.Message;
                return false;
            }
        }

        private async Task ParseCandidateWithVacancy(OpenXmlElement table)
        {
            var rows = table.ChildElements.Where(e => e.LocalName == "tr");
            var name = rows.ElementAt(0).InnerText;
            var vacancyName = name[(name.IndexOf(':') + 2)..];

            currentCandidate = new Candidate
            {
                FullName = ExtractCellTextFromRow(rows, 1, 1)
            };
            try
            {
                var dateStr = ExtractCellTextFromRow(rows, 2, 1);
                currentCandidate.DateOfBirth = dateStr != string.Empty ? Convert.ToDateTime(dateStr) : new DateTime();
            }
            catch
            {
                currentCandidate.DateOfBirth = new DateTime();
            }

            currentCandidate.Address = ExtractCellTextFromRow(rows, 2, 2);
            currentCandidate.Address = currentCandidate.Address[(currentCandidate.Address.IndexOf(':') + 2)..];
            currentCandidate.TelephoneNumber = ExtractCellTextFromRow(rows, 3, 1);
            currentCandidate.MaritalStatus = ExtractCellTextFromRow(rows, 4, 1);

            await _candidateRepository.AddAsync(currentCandidate);
            await _candidateRepository.SaveAsync();

            File.Copy($"{_options.DocumentsSource}\\{_file.Source}", $"{_options.DocumentsSource}\\{currentCandidate.Id}.{currentCandidate.FullName}.docx");
            //await VacancyParse(vacancyName);
            currentVacancy = _vacancyRepository.GetAll().Where(v => v.Name == vacancyName).FirstOrDefault();
            if (currentVacancy == null)
            {
                currentVacancy = new Vacancy() { Name = vacancyName };
                await _vacancyRepository.AddAsync(currentVacancy);
                await _vacancyRepository.SaveAsync();
            }

            var candidateVacancy = new CandidateVacancy()
            {
                CandidateId = currentCandidate.Id,
                VacancyId = currentVacancy.Id
            };
            await _candidateVacancyRepository.AddAsync(candidateVacancy);
            await _candidateVacancyRepository.SaveAsync();
        }

        private async Task VacancyParse(string vacancyName)
        {
            currentVacancy = _vacancyRepository.GetAll().Where(v => v.Name == vacancyName).FirstOrDefault();
            if (currentVacancy == null)
            {
                currentVacancy = new Vacancy() { Name = vacancyName };
                await _vacancyRepository.AddAsync(currentVacancy);
                await _vacancyRepository.SaveAsync();
            }
            var candidateVacancy = new CandidateVacancy()
            {
                CandidateId = currentCandidate.Id,
                VacancyId = currentVacancy.Id
            };
            await _candidateVacancyRepository.AddAsync(candidateVacancy);
            await _candidateVacancyRepository.SaveAsync();
        }

        private static string ExtractCellTextFromRow(IEnumerable<OpenXmlElement> rows, int rowInd, int cellInd)
        {
            return rows
                .ElementAt(rowInd)
                .ChildElements
                .Where(e => e.LocalName == "tc")
                .ElementAt(cellInd).InnerText;
        }

        private async Task ParseQuestionnaire(OpenXmlElement table)
        {
            currentQuestionnaire.CandidateId = currentCandidate.Id;
            currentQuestionnaire.VacancyId = currentVacancy.Id;
            currentQuestionnaire.DocumentFileId = _file.Id;

            await _questionnaireRepository.AddAsync(currentQuestionnaire);
            await _questionnaireRepository.SaveAsync();

            foreach (var child in table.ChildElements.Where(e => e.LocalName == "tr").Skip(1))
            {
                if (child.ChildElements.Count == 3)
                {
                    currentCategory = new QuestionCategory()
                    {
                        Name = child.ChildElements.ElementAt(2).InnerText,
                        QuestionnaireId = currentQuestionnaire.Id
                    };
                    await _questionCategoryRepository.AddAsync(currentCategory);
                    await _questionCategoryRepository.SaveAsync();
                }
                if (child.ChildElements.Count == 5)
                {
                    await ParseQuestion(child);
                }
            }
        }

        private async Task ParseQuestion(OpenXmlElement child)
        {
            var question = new Question
            {
                QuestionCategoryId = currentCategory.Id,
                Name = child.ChildElements[2].InnerText
            };
            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveAsync();
            if (child.ChildElements[4].InnerText != string.Empty)
            {
                var answer = new Answer
                {
                    CandidateId = currentCandidate.Id,
                    QuestionId = question.Id,
                    Comment = child.ChildElements[4].InnerText
                };
                try
                {
                    answer.Estimation = child.ChildElements[3].InnerText == string.Empty ?
                        (byte)0 : Convert.ToByte(child.ChildElements[3].InnerText);
                }
                catch
                { }
                await _answerRepository.AddAsync(answer);
                await _answerRepository.SaveAsync();
            }
        }
    }
}