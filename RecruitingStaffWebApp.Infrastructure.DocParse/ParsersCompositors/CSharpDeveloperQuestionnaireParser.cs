﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors
{
    public class CSharpDeveloperQuestionnaireParser : ParserStrategy
    {
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

        public CSharpDeveloperQuestionnaireParser(IOptions<WebAppOptions> options) : base(options)
        {
        }

        public sealed override async Task<ParsedData> Parse(string fileName)
        {
            using (var wordDoc = WordprocessingDocument.Open($"{_options.DocumentsSource}\\{fileName}", false))
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
                            }
                        }
                    }
                }
            }
            return parsedData;
        }

        private async Task ParseQuestionnaire(OpenXmlElement table)
        {
            parsedData.Questionnaire = new Questionnaire
            {
                Name = questionnaireName,
                VacancyId = parsedData.Vacancy.Id
            };

            foreach (var child in table.ChildElements.Where(e => e.LocalName == "tr").Skip(1))
            {
                await ParseQuestionCategory(child);
                await ParseQuestion(child);
            }
        }

        private async Task ParseCandidate(OpenXmlElement table)
        {
            var rows = table.ChildElements.Where(e => e.LocalName == "tr");
            var vacancyName = rows.ElementAt(0).InnerText;

            parsedData.Candidate = new Candidate
            {
                FullName = ExtractCellTextFromRow(rows, FullNameRow, FullNameColumn)
            };
            PasreDateOfBirth(rows);
            parsedData.Candidate.Address = ExtractCellTextFromRow(rows, AddressRow, AddressColumn);
            parsedData.Candidate.Address = parsedData.Candidate.Address[(parsedData.Candidate.Address.IndexOf(':') + 1)..].Trim();
            parsedData.Candidate.TelephoneNumber = ExtractCellTextFromRow(rows, TelephoneNumberRow, TelephoneNumberColumn);
            parsedData.Candidate.MaritalStatus = ExtractCellTextFromRow(rows, MaritalStatusRow, MaritalStatusColumn);
            await VacancyParse(vacancyName);
        }

        private void PasreDateOfBirth(IEnumerable<OpenXmlElement> rows)
        {
            var dateStr = ExtractCellTextFromRow(rows, DateOfBirthRow, DateOfBirthColumn);
            _ = DateTime.TryParse(dateStr, out var DateOfBirth);
            parsedData.Candidate.DateOfBirth = DateOfBirth;
        }

        private Task VacancyParse(string vacancyName)
        {
            vacancyName = vacancyName[(vacancyName.IndexOf(':') + 1)..];
            vacancyName = vacancyName.Trim(' ');
            parsedData.Vacancy = new Vacancy() { Name = vacancyName };
            return Task.CompletedTask;
        }

        private Task ParseQuestionCategory(OpenXmlElement child)
        {
            if (child.ChildElements.Count == 3)
            {
                currentCategory = new QuestionCategory()
                {
                    Name = child.ChildElements.ElementAt(2).InnerText,
                    Questionnaire = parsedData.Questionnaire
                };
                parsedData.QuestionCategories.Add(currentCategory);
            }
            return Task.CompletedTask;
        }

        private async Task ParseQuestion(OpenXmlElement child)
        {
            if (child.ChildElements.Count == 5)
            {
                currentQuestion = new Question
                {
                    QuestionCategory = currentCategory,
                    Name = child.ChildElements[2].InnerText
                };
                parsedData.Questions.Add(currentQuestion);
                await ParseAnswer(child);
            }
        }

        private Task ParseAnswer(OpenXmlElement child)
        {
            if (child.ChildElements[4].InnerText != string.Empty)
            {
                var answer = new Answer
                {
                    Candidate = parsedData.Candidate,
                    Question = currentQuestion,
                    Text = child.ChildElements[4].InnerText
                };
                try
                {
                    answer.Estimation = child.ChildElements[3].InnerText == string.Empty ?
                        (byte)0 : Convert.ToByte(child.ChildElements[3].InnerText);
                }
                catch
                { }
                parsedData.Answers.Add(answer);
            }
            return Task.CompletedTask;
        }
    }
}

