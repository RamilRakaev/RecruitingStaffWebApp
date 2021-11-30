using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaffWebApp.Services.DocParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse.ParsersCompositors
{
    public class OfficeQuestionnaireParser : ParserStrategy
    {
        private PreviousJob currentPreviousJob;
        private IEnumerable<OpenXmlElement> rows;
        public OfficeQuestionnaireParser(WebAppOptions options) : base(options)
        {
            parsedData.Candidate = new Candidate()
            {
                PreviousJobs = new List<PreviousJob>(),
                Educations = new List<Education>()
            };
        }

        public async override Task<ParsedData> Parse(string fileName)
        {
            using (var wordDoc = WordprocessingDocument.Open($"{_options.DocumentsSource}\\{fileName}", false))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;

                var table = body.ChildElements.Where(e => e.LocalName == "tbl").First();

                rows = table.ExtractRowsFromTable();

                await ParseEducation(8);
                await ParsePreviousJobs(12);
                await ParseQuestions(34);
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
            var title = rows.ExtractCellTextFromRow(index, 2);
            if (title != string.Empty)
            {
                Education education = new()
                {
                    EducationalInstitutionName = title,
                    StartDateOfTraining = rows.TryExtractDate(index, 1),
                    EndDateOfTraining = rows.TryExtractDate(index, 2),
                };
                var specialtyAndQualification = rows.ExtractCellTextFromRow(index, 3);
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
            var name = rows.ExtractCellTextFromRow(index, 1);
            if (name != string.Empty)
            {
                var startDate = rows.TryExtractDate(index + 3, 0);
                var endDate = rows.TryExtractDate(index + 3, 1);
                var responsibilities = rows.ExtractCellTextFromRow(index + 2, 1, @"([\n,:])(\W*\w*\W*)*");
                responsibilities = Regex.Replace(responsibilities, @"[\n:]", "");

                PreviousJob previousJob = new()
                {
                    OrganizationName = name,
                    PositionAtWork = rows.ExtractCellTextFromRow(index + 1, 1),
                    Salary = rows.ExtractTextAfterCharacterFromRow(index + 1, 2, ' '),
                    DateOfStart = startDate,
                    DateOfEnd = endDate,
                    Responsibilities = responsibilities,
                    LeavingReason = rows.ExtractCellTextFromRow(index + 4, 1),
                };
                var organizationPhoneNumberAndAddress = rows
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

        private Task ParseRecommender(int index)
        {
            var name = rows.ExtractCellTextFromRow(index + 6, 1);
            if (name != "" || name == null)
            {
                Recommender recommender = new()
                {
                    FullName = rows.ExtractCellTextFromRow(index, 1)
                };
                currentPreviousJob.Recommenders.Add(recommender);
            }
            return Task.CompletedTask;
        }

        private Task ParseQuestions(int start)
        {
            QuestionCategory questionCategory = new()
            {
                Name = rows.ElementAt(start).InnerText,
                Questions = new()
            };
            for (int i = 0; i < rows.Count(); i++)
            {
                if (rows.ElementAt(i).ExtractParagraphsFromRow().Any())
                {
                    break;
                }
                questionCategory.Questions.Add(new Question()
                {
                    Name = rows.ElementAt(i).ExtractCellTextFromRow(i, 0)
                });
            }
            return Task.CompletedTask;
        }
    }
}
