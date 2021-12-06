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
        private QuestionCategory currentQuestionCategory;
        private PreviousJob currentPreviousJob;
        private IEnumerable<OpenXmlElement> currentRows;

        private const string questionnaireName = "Анкета Офис";
        private const string salaryQuestionCategoryName = "Зарплата";
        private const string otherQuestionCategoryName = "Другое";

        public OfficeQuestionnaireParser(WebAppOptions options) : base(options)
        {
            parsedData.Questionnaire = new Questionnaire()
            {
                Name = questionnaireName,
                QuestionCategories = new(),
            };
        }

        public async override Task<ParsedData> Parse(string fileName)
        {
            using (var wordDoc = WordprocessingDocument.Open($"{_options.DocumentsSource}\\{fileName}", false))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;

                var tables = body.ChildElements.Where(e => e.LocalName == "tbl");
                var paragraphs = body.ChildElements.Where(e => e.LocalName == "p" && e.InnerText != "");
                currentRows = tables.First().ExtractRowsFromTable(false);

                await ParseVacancy(0);
                await ParseCandidate(1);
                await ParseEducation(9);
                await ParseEducation(10);
                await ParsePreviousJobs(13);
                int startRow = 35;
                await ParseQuestionCategory(ref startRow);
                await ParseQuestionCategory(ref startRow);
                await ParseOtherQuestions(ref startRow);
                await ParseSalaryQuestions(ref startRow);

                currentRows = tables.Last().ExtractRowsFromTable(false);
                startRow = 0;
                await ParseQuestionsFromDoubleTable(ref startRow, categoryName: paragraphs.ElementAt(1).InnerText);
                return parsedData;
            }
        }

        private Task ParseCandidate(in int index)
        {
            parsedData.Candidate = new Candidate()
            {
                FullName = currentRows.ExtractCellTextFromRow(index, 1),
                DateOfBirth = currentRows.TryExtractDate(index + 1, 1),
                PlaceOfBirth = currentRows.ExtractCellTextFromRow(index + 1, 2),
                AddressIndex = currentRows.ExtractCellTextFromRow(index + 2, 1),
                Address = currentRows.ExtractCellTextFromRow(index + 3, 1),
                TelephoneNumber = currentRows.ExtractCellTextFromRow(index + 4, 1),
                MaritalStatus = currentRows.ExtractCellTextFromRow(index + 5, 1),
                Answers = new(),
            };
            parsedData.Candidate.EmailAddress = Regex.Replace(
                currentRows.ExtractCellTextFromRow(index + 4, 2, @"(E-Mail).*"),
                "E-Mail",
                "");
            var placeOfBirth = currentRows.ExtractCellTextFromRow(index + 1, 2).ToLower();
            parsedData.Candidate.PlaceOfBirth = Regex.Replace(placeOfBirth, @"(место рождения)\W?", "");
            ParseKids(index + 5);
            return Task.CompletedTask;
        }

        private Task ParseVacancy(in int index)
        {
            var name = Regex.Replace(
                currentRows.ExtractCellTextFromRow(index, 1)
                .ToLower(),
                @"вакансия\W*",
                "");
            parsedData.Vacancy = new Vacancy()
            {
                Name = name
            };
            return Task.CompletedTask;
        }

        private Task ParseKids(in int index)
        {
            parsedData.Candidate.Kids = new();
            var kids = currentRows.ExtractCellTextFromRow(index, 2).ToLower();
            var kidsArray = FindMatches(kids, @"[^(](\b[\w]+\b)\W*,{1}\W*(\b[\w]+\b)");
            foreach (var kid in kidsArray)
            {
                var properties = kid.Split(',');
                _ = int.TryParse(properties[1], out var age);
                parsedData.Candidate.Kids.Add(new()
                {
                    Age = age,
                    Gender = properties[0].Trim(' '),
                });
            }
            return Task.CompletedTask;
        }

        private Task ParseEducation(in int index)
        {
            parsedData.Candidate.Educations = new();
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
                parsedData.Candidate.Educations.Add(education);
            }
            return Task.CompletedTask;
        }

        private async Task ParsePreviousJobs(int index)
        {
            parsedData.Candidate.PreviousJobs = new();
            await ParsePreviousJob(index);
            await ParsePreviousJob(index + 7);
            await ParsePreviousJob(index + 15);
        }

        private async Task ParsePreviousJob(int index)
        {
            parsedData.Candidate.PreviousJobs = new();
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
                await ParseRecommender(index);
            }
        }

        private Task ParseRecommender(in int index)
        {
            currentPreviousJob.Recommenders = new();
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
            parsedData.Questionnaire.QuestionCategories.Add(currentQuestionCategory);
            ParseQuestionsFromTable(ref startRow, columnIndex);
            return Task.CompletedTask;
        }

        private Task ParseQuestionsFromTable(ref int currentRow, int columnIndex = 1)
        {
            for (++currentRow; currentRow < currentRows.Count(); currentRow++)
            {
                var cells = currentRows.ElementAt(currentRow).ExtractCellsFromRow();
                if (cells.Count() == 1)
                {
                    break;
                }
                var text = cells.ElementAt(columnIndex).InnerText;
                if (text != "")
                {
                    var question = new Question()
                    {
                        Name = text,
                    };
                    ParseAnswer(cells.ElementAt(columnIndex == 1 ? 0 : 1).InnerText, question);
                    currentQuestionCategory.Questions.Add(question);
                }
            }
            return Task.CompletedTask;
        }

        private Task ParseAnswer(in string textAnswer, Question question)
        {
            parsedData.AnswersOnQuestions.Add(question, new Answer() { Text = textAnswer });
            return Task.CompletedTask;
        }

        private Task ParseOtherQuestions(ref int startRow)
        {
            QuestionCategory questionCategory = new()
            {
                Name = otherQuestionCategoryName,
                Questions = new()
            };
            parsedData.Questionnaire.QuestionCategories.Add(questionCategory);
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
                    var question = new Question()
                    {
                        Name = text.FindText(@".*[?]"),
                    };
                    questionCategory.Questions.Add(question);
                    ParseAnswer(text.FindText(@"[?].*", @"[?]"), question);
                }
            }
            return Task.CompletedTask;
        }

        private Task ParseSalaryQuestions(ref int startRow)
        {
            var cells = currentRows.ElementAt(startRow).ExtractCellsFromRow();
            var wishesAboutQuestion = new Question()
            {
                Name = cells.ElementAt(0).InnerText
            };
            var formOfPayment = new Question()
            {
                Name = cells.ElementAt(2).InnerText
            };
            ParseAnswer(cells.ElementAt(1).InnerText, wishesAboutQuestion);
            ParseAnswer(cells.ElementAt(3).InnerText, formOfPayment);
            QuestionCategory questionCategory = new()
            {
                Name = salaryQuestionCategoryName,
                Questions = new()
                {
                    wishesAboutQuestion,
                    formOfPayment
                },
            };
            parsedData.Questionnaire.QuestionCategories.Add(questionCategory);
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
            parsedData.Questionnaire.QuestionCategories.Add(currentQuestionCategory);
            for (; ; startRow++)
            {
                var cells = currentRows.ElementAt(startRow).ExtractCellsFromRow();
                if (cells.Count() == 2)
                {
                    var firstCell = cells.ElementAt(0).InnerText;
                    var secondCell = cells.ElementAt(1).InnerText;
                    var pattern = @".*([:])";
                    var firstQuestion = new Question()
                    {
                        Name = firstCell.FindText(pattern)
                    };
                    var secondQuestion = new Question()
                    {
                        Name = secondCell.FindText(pattern)
                    };
                    currentQuestionCategory.Questions.Add(firstQuestion);
                    currentQuestionCategory.Questions.Add(secondQuestion);
                    pattern = @"([:].*)";
                    var removementPattern = @"[:]";
                    ParseAnswer(firstCell.FindText(pattern, removementPattern), firstQuestion);
                    ParseAnswer(firstCell.FindText(pattern, removementPattern), secondQuestion);
                    continue;
                }
                break;
            }
            ParseQuestionsFromTable(ref startRow, 0);
            return Task.CompletedTask;
        }

        private static string[] FindMatches(string input, string pattern)
        {
            Regex regex = new(pattern);
            var matches = regex.Matches(input);
            return matches.Select(m => m.Value).ToArray();
        }
    }
}
