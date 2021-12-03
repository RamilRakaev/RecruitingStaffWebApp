using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
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

        public async Task SaveParsedData(ParsedData parsedData, bool parseAnswers = false)
        {
            parsedData.Questionnaire.VacancyId = parsedData.Vacancy.Id;
            await _mediator.Send(new CreateOrChangeQuestionnaireCommand(parsedData.Questionnaire));
            if (parseAnswers)
            {
                await _mediator.Send(
                    new CreateOrChangeCandidateCommand(
                    parsedData.Candidate,
                    parsedData.Vacancy.Id,
                    parsedData.Questionnaire.Id));
                await CreateCandidateDocument(parsedData, parsedData.Candidate.Id, parsedData.Questionnaire.Id);
            }
            else
            {
                await CreateQuestionnaireDocument(parsedData);
            }
        }

        private async Task CreateCandidateDocument(ParsedData parsedData, int candidateId, int questionnaireId)
        {
            File = new()
            {
                Source = $"{candidateId}.{parsedData.Candidate.FullName}{parsedData.FileExtension}",
                FileType = FileType.Questionnaire,
                CandidateId = candidateId,
                QuestionnaireId = questionnaireId,
            };
            await _mediator.Send(new CreateRecruitingStaffWebAppFileCommand(File));
        }

        private async Task CreateQuestionnaireDocument(ParsedData parsedData)
        {
            File = new()
            {
                Source = $"{parsedData.Questionnaire.Id}.{parsedData.Questionnaire.Name}{parsedData.FileExtension}",
                FileType = FileType.Questionnaire,
                QuestionnaireId = parsedData.Questionnaire.Id,
            };
            await _mediator.Send(new CreateRecruitingStaffWebAppFileCommand(File));
        }
    }
}
