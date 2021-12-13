﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using RecruitingStaffWebApp.Services.DocParse;
using RecruitingStaffWebApp.Services.DocParse.Model;
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
            parsedData.AddQuestionnaire(questionnaireName);
        }

        public async override Task<ParsedData> Parse(string path)
        {
            using var wordDoc = WordprocessingDocument.Open(path, false);
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
            await ParseOtherQuestionCategory(ref startRow);
            await ParseSalaryQuestions(ref startRow);

            currentRows = tables.Last().ExtractRowsFromTable(false);
            startRow = 0;
            await ParseQuestionCategoryFromDoubleTable(ref startRow, categoryName: paragraphs.ElementAt(1).InnerText);
            return parsedData;

        }

        private Task ParseCandidate(in int index)
        {
            parsedData.Candidate = new()
            {
                FullName = currentRows.ExtractCellTextFromRow(index, 1),
                DateOfBirth = currentRows.TryExtractDate(index + 1, 1),
                PlaceOfBirth = currentRows.ExtractCellTextFromRow(index + 1, 2),
                AddressIndex = currentRows.ExtractCellTextFromRow(index + 2, 1),
                Address = currentRows.ExtractCellTextFromRow(index + 3, 1),
                TelephoneNumber = currentRows.ExtractCellTextFromRow(index + 4, 1),
                MaritalStatus = currentRows.ExtractCellTextFromRow(index + 5, 1),
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
            parsedData.Vacancy = new()
            {
                Name = name
            };
            return Task.CompletedTask;
        }

        private Task ParseKids(in int index)
        {
            var kids = currentRows.ExtractCellTextFromRow(index, 2).ToLower();
            var kidsArray = FindMatches(kids, @"[^(](\b[\w]+\b)\W*,{1}\W*(\b[\w]+\b)");
            foreach (var kid in kidsArray)
            {
                var properties = kid.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
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
            await ParsePreviousJob(index);
            await ParsePreviousJob(index + 7);
            await ParsePreviousJob(index + 15);
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
                parsedData.Candidate.AddPreviousJob(previousJob);
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
                    FullName = currentRows.ExtractCellTextFromRow(index, 1),
                };
                parsedData.Candidate.AddRecommender(recommender);
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
            parsedData.AddQuestionCategory(categoryName);
            ParseQuestionsFromTable(ref startRow, columnIndex);
            return Task.CompletedTask;
        }

        private Task ParseOtherQuestionCategory(ref int startRow)
        {
            parsedData.AddQuestionCategory(otherQuestionCategoryName);
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
                    parsedData.AddQuestion(text.FindText(@".*[?]"));
                    parsedData.AddAnswer(text.FindText(@"[?].*", @"[?]"));
                }
            }
            return Task.CompletedTask;
        }

        private Task ParseSalaryQuestions(ref int startRow)
        {
            parsedData.AddQuestionCategory(salaryQuestionCategoryName);
            var cells = currentRows.ElementAt(startRow).ExtractCellsFromRow();
            parsedData.AddQuestion(cells.ElementAt(0).InnerText);
            parsedData.AddAnswer(cells.ElementAt(1).InnerText);
            parsedData.AddQuestion(cells.ElementAt(2).InnerText);
            parsedData.AddAnswer(cells.ElementAt(3).InnerText);
            startRow++;
            return Task.CompletedTask;
        }

        public Task ParseQuestionCategoryFromDoubleTable(ref int startRow, string categoryName)
        {
            parsedData.AddQuestionCategory(categoryName);
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

                    parsedData.AddQuestion(firstCell.FindText(questionPattern));
                    parsedData.AddAnswer(firstCell.FindText(answerPattern, removementPattern));
                    parsedData.AddQuestion(secondCell.FindText(questionPattern));
                    parsedData.AddAnswer(firstCell.FindText(answerPattern, removementPattern));
                    continue;
                }
                break;
            }
            ParseQuestionsFromTable(ref startRow, 0);
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
                    parsedData.AddQuestion(text);
                    parsedData.AddAnswer(cells.ElementAt(columnIndex == 1 ? 0 : 1).InnerText);
                }
            }
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
