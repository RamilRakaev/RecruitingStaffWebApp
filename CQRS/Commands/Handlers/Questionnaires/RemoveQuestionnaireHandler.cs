using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class RemoveQuestionnaireHandler : IRequestHandler<RemoveQuestionnaireCommand, bool>
    {
        private readonly IMediator _mediator;

        public RemoveQuestionnaireHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(RemoveQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var questionCategories = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<QuestionCategory>(
                    q => q.QuestionnaireId == request.QuestionnaireId),
                cancellationToken);
            foreach (var questionCategory in questionCategories)
            {
                await _mediator.Send(new RemoveQuestionCategoryCommand(questionCategory.Id),
                    cancellationToken);
            }
            var files = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<RecruitingStaffWebAppFile>(
                    f => f.QuestionnaireId == request.QuestionnaireId),
                cancellationToken);
            foreach (var file in files)
            {
                await _mediator.Send(new RemoveEntityCommand<RecruitingStaffWebAppFile>(file.Id),
                    cancellationToken);
            }
            var candidateQuestionnaires = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<CandidateQuestionnaire>(cq => cq.SecondEntityId == request.QuestionnaireId),
                cancellationToken);
            foreach(var candidateQuestionnaire in candidateQuestionnaires)
            {
                await _mediator.Send(new RemoveEntityCommand<CandidateQuestionnaire>(candidateQuestionnaire.Id),
                    cancellationToken);
            }
            await _mediator.Send(new RemoveEntityCommand<Questionnaire>(request.QuestionnaireId),
                cancellationToken);
            return true;
        }
    }
}
