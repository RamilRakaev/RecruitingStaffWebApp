using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.Educations;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.PreviousJobs;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.Recommenders;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaffWebApp.Services.DocParse;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse
{
    public class QuestionnaireDbManager
    {
        private readonly IMediator _mediator;

        public QuestionnaireDbManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public RecruitingStaffWebAppFile File { get; private set; }

        public async Task SaveParsedData(ParsedData parsedData)
        {
            var vacancy = await _mediator.Send(new CreateOrChangeVacancyCommand(parsedData.Vacancy));

            parsedData.Questionnaire.VacancyId = vacancy.Id;
            var questionnaire = await _mediator.Send(new CreateOrChangeQuestionnaireCommand(parsedData.Questionnaire));

            var candidate = await _mediator.Send(
                new CreateOrChangeCandidateCommand(
                parsedData.Candidate,
                parsedData.Vacancy.Id,
                parsedData.Questionnaire.Id));
            await CreateFile(parsedData, candidate.Id, questionnaire.Id);
        }

        private async Task CreateFile(ParsedData parsedData, int candidateId, int questionnaireId)
        {
            File = new RecruitingStaffWebAppFile()
            {
                Source = $"{candidateId}.{parsedData.Candidate.FullName}{parsedData.FileExtension}",
                FileType = FileType.Questionnaire,
                CandidateId = candidateId,
                QuestionnaireId = questionnaireId,
            };
            await _mediator.Send(new CreateRecruitingStaffWebAppFileCommand(File));
        }
    }
}
