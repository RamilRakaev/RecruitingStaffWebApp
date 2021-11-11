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
        private readonly IRepository<CandidateQuestionnaire> _candidateQuestionnaire;
        private readonly IRepository<Questionnaire> _questionnaireRepository;
        private readonly IRepository<QuestionCategory> _questionCategoryRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly WebAppOptions _options;

        private string _fileName;
        private string questionnaireName;
        private RecruitingStaffWebAppFile _file;
        private Vacancy currentVacancy;
        private Candidate currentCandidate;
        private Questionnaire currentQuestionnaire;
        private QuestionCategory currentCategory;
        private Question currentQuestion;

        private const int DateOfBirthRow = 2;
        private const int DateOfBirthColumn = 1;

        private const int FullNameRow = 1;
        private const int FullNameColumn = 1;

        private const int AddressRow = 2;
        private const int AddressColumn = 2;

        private const int TelephoneNumberRow = 3;
        private const int TelephoneNumberColumn = 1;

        private const int MaritalStatusRow = 4;
        private const int MaritalStatusColumn = 1;

        public QuestionnaireManager(
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IRepository<Vacancy> vacancyRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<CandidateVacancy> candidateVacancyRepository,
            IRepository<CandidateQuestionnaire> candidateQuestionnaire,
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
            _candidateQuestionnaire = candidateQuestionnaire;
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
                _fileName = fileName;
                await Parse();
                await SaveToFile();
                File.Delete($"{_options.DocumentsSource}\\{_fileName}");
                return true;
            }
            catch (Exception e)
            {
                Exception = e.Message;
                return false;
            }
        }

        private async Task Parse()
        {
            using (var wordDoc = WordprocessingDocument.Open($"{_options.DocumentsSource}\\{_fileName}", false))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;
                questionnaireName = body.ChildElements.Where(e => e.LocalName == "p").FirstOrDefault().InnerText;

                foreach (var element in body.ChildElements.Where(e => e.LocalName == "tbl"))
                {
                    await ParseCandidate(element);
                    foreach (var row in element.ChildElements.Reverse())
                    {
                        foreach (var cell in row.ChildElements)
                        {
                            var table = cell.FirstOrDefault(c => c.LocalName == "tbl");
                            if (table != null)
                            {
                                await ParseQuestionnaire(table);
                                await ParseCandidateQuestionnaire();
                            }
                        }
                    }
                }
            }
        }

        private async Task ParseQuestionnaire(OpenXmlElement table)
        {
            currentQuestionnaire = _questionnaireRepository
                .GetAll()
                .Where(q => q.Name == questionnaireName)
                .FirstOrDefault();

            if (currentQuestionnaire == null)
            {
                currentQuestionnaire = new Questionnaire
                {
                    Name = questionnaireName,
                    VacancyId = currentVacancy.Id
                };

                await _questionnaireRepository.AddAsync(currentQuestionnaire);
                await _questionnaireRepository.SaveAsync();
            }
            foreach (var child in table.ChildElements.Where(e => e.LocalName == "tr").Skip(1))
            {
                await ParseQuestionCategory(child);
                await ParseQuestion(child);
            }
        }

        private async Task ParseCandidateQuestionnaire()
        {
            var candidateQuestionnaire = new CandidateQuestionnaire()
            {
                QuestionnaireId = currentQuestionnaire.Id,
                CandidateId = currentCandidate.Id
            };
            await _candidateQuestionnaire.AddAsync(candidateQuestionnaire);
            await _candidateQuestionnaire.SaveAsync();
        }

        private async Task ParseCandidate(OpenXmlElement table)
        {
            var rows = table.ChildElements.Where(e => e.LocalName == "tr");
            var name = rows.ElementAt(0).InnerText;
            var vacancyName = name[(name.IndexOf(':') + 2)..];

            currentCandidate = new Candidate
            {
                FullName = ExtractCellTextFromRow(rows, FullNameRow, FullNameColumn)
            };
            PasreDateOfBirth(rows);
            currentCandidate.Address = ExtractCellTextFromRow(rows, AddressRow, AddressColumn);
            currentCandidate.Address = currentCandidate.Address[(currentCandidate.Address.IndexOf(':') + 2)..];
            currentCandidate.TelephoneNumber = ExtractCellTextFromRow(rows, TelephoneNumberRow, TelephoneNumberColumn);
            currentCandidate.MaritalStatus = ExtractCellTextFromRow(rows, MaritalStatusRow, MaritalStatusColumn);

            await _candidateRepository.AddAsync(currentCandidate);
            await _candidateRepository.SaveAsync();

            await VacancyParse(vacancyName);
        }

        private void PasreDateOfBirth(IEnumerable<OpenXmlElement> rows)
        {
            try
            {
                var dateStr = ExtractCellTextFromRow(rows, DateOfBirthRow, DateOfBirthColumn);
                currentCandidate.DateOfBirth = dateStr != string.Empty ? Convert.ToDateTime(dateStr) : new DateTime();
            }
            catch
            {
                currentCandidate.DateOfBirth = new DateTime();
            }
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

        private async Task ParseQuestionCategory(OpenXmlElement child)
        {
            if (child.ChildElements.Count == 3)
            {
                var name = child.ChildElements.ElementAt(2).InnerText;
                currentCategory = _questionCategoryRepository.GetAll().Where(qc => qc.Name == name && qc.QuestionnaireId == currentQuestionnaire.Id).FirstOrDefault();
                if (currentCategory == null)
                {
                    currentCategory = new QuestionCategory()
                    {
                        Name = child.ChildElements.ElementAt(2).InnerText,
                        QuestionnaireId = currentQuestionnaire.Id
                    };
                    await _questionCategoryRepository.AddAsync(currentCategory);
                    await _questionCategoryRepository.SaveAsync();
                }
            }
        }

        private async Task ParseQuestion(OpenXmlElement child)
        {
            if (child.ChildElements.Count == 5)
            {
                var name = child.ChildElements[2].InnerText;
                currentQuestion = _questionRepository.GetAll().Where(q => q.Name == name && q.QuestionCategoryId == currentCategory.Id).FirstOrDefault();
                if (currentQuestion == null)
                {
                    currentQuestion = new Question
                    {
                        QuestionCategoryId = currentCategory.Id,
                        Name = child.ChildElements[2].InnerText
                    };
                    await _questionRepository.AddAsync(currentQuestion);
                    await _questionRepository.SaveAsync();
                }
                await ParseAnswer(child);
            }
        }

        private async Task ParseAnswer(OpenXmlElement child)
        {
            if (child.ChildElements[4].InnerText != string.Empty)
            {
                var answer = new Answer
                {
                    CandidateId = currentCandidate.Id,
                    QuestionId = currentQuestion.Id,
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

        private async Task SaveToFile()
        {
            _file = new RecruitingStaffWebAppFile()
            {
                Source = $"{currentCandidate.Id}.{currentCandidate.FullName}.docx",
                FileType = FileType.Questionnaire,
                CandidateId = currentCandidate.Id,
                QuestionnaireId = currentQuestionnaire.Id
            };
            await _fileRepository.AddAsync(_file);
            await _fileRepository.SaveAsync();

            File.Copy($"{_options.DocumentsSource}\\{_fileName}", $"{_options.DocumentsSource}\\{_file.Source}");
        }

    }
}