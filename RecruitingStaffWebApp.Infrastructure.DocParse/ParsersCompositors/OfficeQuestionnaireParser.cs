using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaffWebApp.Services.DocParse;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors
{
    public class OfficeQuestionnaireParser : ParserStrategy
    {
        private Questionnaire currentQuestionnaire;
        private QuestionCategory currentQuestionCategory;
        private PreviousJob currentPreviousJob;
        private IEnumerable<OpenXmlElement> currentRows;

        private const string questionnaireName = "Анкета Офис";
        private const string salaryQuestionCategoryName = "Зарплата";
        private const string otherQuestionCategoryName = "Другое";

        public OfficeQuestionnaireParser(WebAppOptions options) : base(options)
        {
            parsedData.Candidate = new Candidate()
            {
                PreviousJobs = new List<PreviousJob>(),
                Educations = new List<Education>()
            };
            currentQuestionnaire = new Questionnaire()
            {
                Name = questionnaireName,
                QuestionCategories = new()
            };
        }

        public async override Task<ParsedData> Parse(string fileName)
        {
            using (var wordDoc = WordprocessingDocument.Open($"{_options.DocumentsSource}\\{fileName}", false))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;

                var tables = body.ChildElements.Where(e => e.LocalName == "tbl");
                var paragraphs = body.ChildElements.Where(e => e.LocalName == "p" && e.InnerText != "");
                currentRows = tables.First().ExtractRowsFromTable();

                await ParseEducation(8);
                await ParsePreviousJobs(12);
                int startRow = 34;
                await ParseQuestionCategory(ref startRow);
                await ParseQuestionCategory(ref startRow);
                await ParseOtherQuestions(ref startRow);
                await ParseSalaryQuestions(ref startRow);

                currentRows = tables.Last().ExtractRowsFromTable(false);
                startRow = 0;
                await ParseQuestionsFromDoubleTable(ref startRow, categoryName: paragraphs.ElementAt(1).InnerText);
            }
            return null;
        }

        private async Task ParsePreviousJobs(int index)
        {
            await ParsePreviousJob(index);
            await ParsePreviousJob(index + 7);
            await ParsePreviousJob(index + 15);
        }
        private Task ParseEducation(int index)
        {
            var title = currentRows.ExtractCellTextFromRow(index, 2);
            if (title != string.Empty)
            {
                Education education = new()
                {
                    EducationalInstitutionName = title,
                    StartDateOfTraining = currentRows.TryExtractDate(index, 1),
                    EndDateOfTraining = currentRows.TryExtractDate(index, 2),
                };
                var specialtyAndQualification = currentRows.ExtractCellTextFromRow(index, 3);
                if (specialtyAndQualification.Contains(','))
                {
                    education.Specialization = specialtyAndQualification.GetTextBeforeCharacter(',');
                    education.Qualification = specialtyAndQualification.GetTextAfterCharacter(',');
                }
                else
                {
                    education.Specialization = specialtyAndQualification;
                }
                parsedData.Educations.Add(education);
            }
            return Task.CompletedTask;
        }

        private async Task ParsePreviousJob(int index)
        {
            var name = currentRows.ExtractCellTextFromRow(index, 1);
            if (name != string.Empty)
            {
                var startDate = currentRows.TryExtractDate(index + 3, 0);
                var endDate = currentRows.TryExtractDate(index + 3, 1);
                var responsibilities = currentRows.ExtractCellTextFromRow(index + 2, 1, @"([\n,:])(\W*\w*\W*)*");
                responsibilities = Regex.Replace(responsibilities, @"[\n:]", "");

                PreviousJob previousJob = new()
                {
                    OrganizationName = name,
                    PositionAtWork = currentRows.ExtractCellTextFromRow(index + 1, 1),
                    Salary = currentRows.ExtractTextAfterCharacterFromRow(index + 1, 2, ' '),
                    DateOfStart = startDate,
                    DateOfEnd = endDate,
                    Responsibilities = responsibilities,
                    LeavingReason = currentRows.ExtractCellTextFromRow(index + 4, 1),
                };
                var organizationPhoneNumberAndAddress = currentRows
                    .ExtractCellTextFromRow(index, 2)
                    .GetTextAfterCharacter(' ', 4);
                if (organizationPhoneNumberAndAddress.Contains(','))
                {
                    previousJob.OrganizationPhoneNumber = organizationPhoneNumberAndAddress.GetTextBeforeCharacter(',');
                    previousJob.OrganizationAddress = organizationPhoneNumberAndAddress.GetTextAfterCharacter(',');
                }
                parsedData.Candidate.PreviousJobs.Add(previousJob);
                currentPreviousJob = previousJob;
                currentPreviousJob.Recommenders = new List<Recommender>();
                await ParseRecommender(index);
            }
        }

        private Task ParseRecommender(in int index)
        {
            var name = currentRows.ExtractCellTextFromRow(index + 6, 1);
            if (name != "" || name == null)
            {
                Recommender recommender = new()
                {
                    FullName = currentRows.ExtractCellTextFromRow(index, 1)
                };
                currentPreviousJob.Recommenders.Add(recommender);
            }
            return Task.CompletedTask;
        }

        private Task ParseQuestionCategory(ref int startRow, in int columnIndex = 1)
        {
            var categoryName = currentRows.ElementAt(startRow).InnerText;
            while (categoryName == "")
            {
                categoryName = currentRows.ElementAt(++startRow).InnerText;
            }
            currentQuestionCategory = new()
            {
                Name = categoryName,
                Questions = new()
            };
            currentQuestionnaire.QuestionCategories.Add(currentQuestionCategory);
            ParseQuestionsFromTable(ref startRow, columnIndex);
            return Task.CompletedTask;
        }

        private Task ParseQuestionsFromTable(ref int currentRow, int columnIndex = 1)
        {
            for (++currentRow; currentRow < currentRows.Count() ; currentRow++)
            {
                var cells = currentRows.ElementAt(currentRow).ExtractCellsFromRow();
                if (cells.Count() == 1)
                {
                    break;
                }
                var text = cells.ElementAt(columnIndex).InnerText;
                if (text != "")
                {
                    currentQuestionCategory.Questions.Add(new Question()
                    {
                        Name = text
                    });
                }
            }
            return Task.CompletedTask;
        }

        private Task ParseOtherQuestions(ref int startRow)
        {
            QuestionCategory questionCategory = new()
            {
                Name = otherQuestionCategoryName,
                Questions = new()
            };
            currentQuestionnaire.QuestionCategories.Add(questionCategory);
            for (int i = startRow; ; i++)
            {
                var cells = currentRows.ElementAt(i).ExtractCellsFromRow();
                if (cells.Count() > 2)
                {
                    startRow = i;
                    break;
                }
                var text = cells.ElementAt(0).InnerText;
                if (text != "")
                {
                    questionCategory.Questions.Add(new Question()
                    {
                        Name = cells.ElementAt(0).InnerText,
                    });
                }
            }

            return Task.CompletedTask;
        }

        private Task ParseSalaryQuestions(ref int startRow)
        {
            var cells = currentRows.ElementAt(startRow).ExtractCellsFromRow();
            QuestionCategory questionCategory = new()
            {
                Name = salaryQuestionCategoryName,
                Questions = new()
                {
                    new Question()
                    {
                        Name = cells.ElementAt(0).InnerText
                    },
                    new Question()
                    {
                        Name = cells.ElementAt(2).InnerText
                    },
                },
            };
            currentQuestionnaire.QuestionCategories.Add(questionCategory);
            startRow++;
            return Task.CompletedTask;
        }

        public Task ParseQuestionsFromDoubleTable(ref int startRow, string categoryName)
        {
            currentQuestionCategory = new()
            {
                Name = categoryName,
                Questions = new()
            };
            currentQuestionnaire.QuestionCategories.Add(currentQuestionCategory);
            for (; ; startRow++)
            {
                var cells = currentRows.ElementAt(startRow).ExtractCellsFromRow();
                if (cells.Count() == 2)
                {
                    var firstCell = cells.ElementAt(0).InnerText;
                    var secondCell = cells.ElementAt(1).InnerText;
                    var pattern = @"(\W*\w+\W*)+([:])";
                    var removementPattern = @"\W*[:]";
                    currentQuestionCategory.Questions.Add(
                        new()
                        {
                            Name = FindText(firstCell, pattern, removementPattern)
                        });
                    currentQuestionCategory.Questions.Add(
                        new()
                        {
                            Name = FindText(secondCell, pattern, removementPattern)
                        });
                    continue;
                }
                break;
            }
            ParseQuestionsFromTable(ref startRow, 0);
            return Task.CompletedTask;
        }

        private string FindText(string input, string pattern, string removementPattern = "")
        {
            Regex regex = new(pattern);
            var matches = regex.Matches(input);
            var text = matches.Any() ? matches.First().Value : "";
            text = removementPattern != "" ? Regex.Replace(text, removementPattern, "") : text;
            return text;
        }
    }
}
