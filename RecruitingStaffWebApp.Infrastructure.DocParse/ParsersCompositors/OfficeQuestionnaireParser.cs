using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using RecruitingStaffWebApp.Services.DocParse;
using RecruitingStaffWebApp.Services.DocParse.Exceptions;
using RecruitingStaffWebApp.Services.DocParse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors
{
    public class OfficeQuestionnaireParser : ParserStrategy
    {
        private IEnumerable<OpenXmlElement> currentRows;

        private const string questionnaireName = "Анкета Офис";
        private const string salaryQuestionCategoryName = "Зарплата";
        private const string otherQuestionCategoryName = "Другое";

        public OfficeQuestionnaireParser()
        {
            parsedData.Questionnaire = QuestionnaireElement.CreateQuestionnaireElement(questionnaireName);
        }

        public override Task<ParsedData> ParseAsync(string path)
        {
            using var wordDoc = WordprocessingDocument.Open(path, false);
            var body = wordDoc.MainDocumentPart.Document.Body;

            var tables = body.ChildElements.Where(e => e.LocalName == "tbl");
            var paragraphs = body.ChildElements.Where(e => e.LocalName == "p" && e.InnerText != "");
            currentRows = tables.First().ExtractRowsFromTable(false);

            parsedData.Vacancy = ParseVacancy(0);
            parsedData.Candidate = ParseCandidate(1);
            parsedData.Candidate.Educations.Add(ParseEducation(9));
            parsedData.Candidate.Educations.Add(ParseEducation(10));
            parsedData.Candidate.PreviousJobs = ParsePreviousJobs(13);
            int startRow = 35;
            parsedData.Questionnaire.AddChildElement(ParseQuestionCategory(ref startRow));
            parsedData.Questionnaire.AddChildElement(ParseQuestionCategory(ref startRow));
            parsedData.Questionnaire.AddChildElement(ParseOtherQuestionCategory(ref startRow));
            parsedData.Questionnaire.AddChildElement(ParseSalaryQuestionCategory(ref startRow));

            currentRows = tables.Last().ExtractRowsFromTable(false);
            startRow = 0;
            parsedData.Questionnaire.AddChildElement(
                ParseQuestionCategoryFromDoubleTable(
                    ref startRow, categoryName: paragraphs.ElementAt(1).InnerText));
            return Task.FromResult(parsedData);
        }

        private Candidate ParseCandidate(in int index)
        {
            Candidate candidate = new()
            {
                Name = currentRows.ExtractCellTextFromRow(index, 1),
                DateOfBirth = currentRows.TryExtractDate(index + 1, 1),
                PlaceOfBirth = currentRows.ExtractCellTextFromRow(index + 1, 2),
                AddressIndex = currentRows.ExtractCellTextFromRow(index + 2, 1),
                Address = currentRows.ExtractCellTextFromRow(index + 3, 1),
                TelephoneNumber = currentRows.ExtractCellTextFromRow(index + 4, 1),
                MaritalStatus = currentRows.ExtractCellTextFromRow(index + 5, 1),
            };
            candidate.EmailAddress = Regex.Replace(
                currentRows.ExtractCellTextFromRow(index + 4, 2, @"(E-Mail).*"),
                "E-Mail",
                "");
            var placeOfBirth = currentRows.ExtractCellTextFromRow(index + 1, 2).ToLower();
            candidate.PlaceOfBirth = Regex.Replace(placeOfBirth, @"(место рождения)\W?", "");
            candidate.Kids = ParseKids(index + 5);
            return candidate;
        }

        private Vacancy ParseVacancy(in int index)
        {
            var name = Regex.Replace(
                currentRows.ExtractCellTextFromRow(index, 1)
                .ToLower(),
                @"вакансия\W*",
                "");
            Vacancy vacancy = new()
            {
                Name = name
            };
            return vacancy;
        }

        private List<Kid> ParseKids(in int index)
        {
            var kidsText = currentRows.ExtractCellTextFromRow(index, 2).ToLower();
            var kidsArray = FindMatches(kidsText, @"[^(](\b[\w]+\b)\W*,{1}\W*(\b[\w]+\b)");
            List<Kid> kids = new(kidsArray.Length);
            foreach (var kid in kidsArray)
            {
                var properties = kid.Split(',', StringSplitOptions.RemoveEmptyEntries);
                _ = int.TryParse(properties[1], out var age);
                kids.Add(new()
                {
                    Age = age,
                    Gender = properties[0].Trim(' '),
                });
            }
            return kids;
        }

        private Education ParseEducation(in int index)
        {
            var title = currentRows.ExtractCellTextFromRow(index, 2);
            if (title != string.Empty)
            {
                var startYear = int.Parse(currentRows.ExtractCellTextFromRow(index, 0));
                var endYear = int.Parse(currentRows.ExtractCellTextFromRow(index, 1));
                Education education = new()
                {
                    Name = title,
                    StartDateOfTraining = new(startYear, 1, 1),
                    EndDateOfTraining = new(endYear, 1, 1),
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
                return education;
            }
            throw new ParseException();
        }

        private List<PreviousJob> ParsePreviousJobs(int index)
        {
            List<PreviousJob> previousJobs = new();
            previousJobs.Add(ParsePreviousJob(index));
            previousJobs.Add(ParsePreviousJob(index + 7));
            previousJobs.Add(ParsePreviousJob(index + 15));
            return previousJobs;
        }

        private PreviousJob ParsePreviousJob(int index)
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
                    Name = name,
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
                previousJob.Recommenders.Add(ParseRecommender(index + 6));
                return previousJob;
            }
            throw new ParseException();
        }

        private Recommender ParseRecommender(in int index)
        {
            var name = currentRows.ExtractCellTextFromRow(index, 1);
            if (name != "" || name == null)
            {
                var data = currentRows.ExtractCellTextFromRow(index, 1).Split(',');
                Recommender recommender = new()
                {
                    Name = data.ElementAtOrDefault(0),
                    PositionAtWork = data.ElementAtOrDefault(1),
                    PhoneNumber = data.ElementAtOrDefault(2),
                };
                return recommender;
            }
            throw new ParseException();
        }

        private QuestionnaireElement ParseQuestionCategory(ref int startRow, in int columnIndex = 1)
        {
            var categoryName = currentRows.ElementAt(startRow).InnerText;
            while (categoryName == "")
            {
                categoryName = currentRows.ElementAt(++startRow).InnerText;
            }
            QuestionnaireElement questionCategory = QuestionnaireElement.CreateQuestionnaireElement(categoryName);
            questionCategory.AddRangeElements(ParseQuestionsFromTable(ref startRow, columnIndex));
            return questionCategory;
        }

        private QuestionnaireElement ParseOtherQuestionCategory(ref int startRow)
        {
            var questionCategory = QuestionnaireElement.CreateQuestionnaireElement(otherQuestionCategoryName);
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
                    questionCategory.CreateChildElement(text.FindText(@".*[?]"));
                    questionCategory.CurrentChildElement.CreateChildElement(text.FindText(@"[?].*", @"[?]"));
                }
            }
            return questionCategory;
        }

        private QuestionnaireElement ParseSalaryQuestionCategory(ref int startRow)
        {
            var cells = currentRows.ElementAt(startRow).ExtractCellsFromRow();
            var questionCategory = QuestionnaireElement.CreateQuestionnaireElement(salaryQuestionCategoryName);
            questionCategory.CreateChildElement(cells.ElementAt(0).InnerText);
            questionCategory.CurrentChildElement.CreateChildElement(cells.ElementAt(1).InnerText);
            questionCategory.CreateChildElement(cells.ElementAt(2).InnerText);
            questionCategory.CurrentChildElement.CreateChildElement(cells.ElementAt(3).InnerText);

            startRow++;
            return questionCategory;
        }

        public QuestionnaireElement ParseQuestionCategoryFromDoubleTable(ref int startRow, string categoryName)
        {
            var questionCategory = QuestionnaireElement.CreateQuestionnaireElement(categoryName);
            for (; ; startRow++)
            {
                var cells = currentRows.ElementAt(startRow).ExtractCellsFromRow();
                if (cells.Count() == 2)
                {
                    var questionPattern = @".*([:])";
                    var answerPattern = @"([:].*)";
                    var removementPattern = @"[:]";
                    var firstCell = cells.ElementAt(0).InnerText;
                    var secondCell = cells.ElementAt(1).InnerText;

                    var question = questionCategory.CreateChildElement(firstCell.FindText(questionPattern));
                    question.CreateChildElement(firstCell.FindText(answerPattern, removementPattern));
                    question = questionCategory.CreateChildElement(secondCell.FindText(questionPattern));
                    question.CreateChildElement(firstCell.FindText(answerPattern, removementPattern));
                    continue;
                }
                break;
            }
            questionCategory.AddRangeElements(ParseQuestionsFromTable(ref startRow, 0));
            return questionCategory;
        }

        private List<QuestionnaireElement> ParseQuestionsFromTable(ref int currentRow, int columnIndex = 1)
        {
            List<QuestionnaireElement> questions = new();
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
                    var question = QuestionnaireElement.CreateQuestionnaireElement(text);
                    question.CreateChildElement(cells.ElementAt(columnIndex == 1 ? 0 : 1).InnerText);
                    questions.Add(question);
                    //parsedData.AddQuestion(text);
                    //parsedData.AddAnswer(cells.ElementAt(columnIndex == 1 ? 0 : 1).InnerText);
                }
            }
            return questions;
        }

        private static string[] FindMatches(string input, string pattern)
        {
            Regex regex = new(pattern);
            var matches = regex.Matches(input);
            return matches.Select(m => m.Value).ToArray();
        }
    }
}
