using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Vacancies
{
    public class RemoveVacancyHandler : IRequestHandler<RemoveVacancyCommand, bool>
    {
        private readonly IMediator _mediator;

        public RemoveVacancyHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(RemoveVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancyQuestionnaires = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<VacancyQuestionnaire>(cq => cq.FirstEntityId == request.VacancyId),
                cancellationToken);
            foreach(var vacancyQuestionnaire in vacancyQuestionnaires)
            {
                var questionnaires = await _mediator.Send(
                    new GetEntitiesByForeignKeyQuery<Questionnaire>(q => q.Id == vacancyQuestionnaire.SecondEntityId),
                    cancellationToken);
                foreach (var questionnaire in questionnaires)
                {
                    await RemoveQuestionnare(questionnaire.Id, request.VacancyId, cancellationToken);
                }
            }
            await RemoveTestTasks(request.VacancyId, cancellationToken);
            await _mediator.Send(new RemoveEntityCommand<Vacancy>(request.VacancyId),
                cancellationToken);
            return true;
        }

        private async Task RemoveQuestionnare(int questionnaireId, int vacancyId, CancellationToken cancellationToken)
        {
            var questionnaresMaps = await _mediator.Send(
                        new GetEntitiesByForeignKeyQuery<VacancyQuestionnaire>(vq =>
                        vq.SecondEntityId == questionnaireId),
                        cancellationToken);
            if (questionnaresMaps.Length == 1)
            {
                await _mediator.Send(new RemoveQuestionnaireCommand(questionnaireId),
                    cancellationToken);
            }
            else
            {
                var map = questionnaresMaps
                    .FirstOrDefault(qm => qm.FirstEntityId == vacancyId
                    && qm.SecondEntityId == questionnaireId);
                await _mediator.Send(new RemoveEntityCommand<VacancyQuestionnaire>(map.Id),
                    cancellationToken);
            }
        }


        private async Task RemoveTestTasks(int vacancyId, CancellationToken cancellationToken)
        {
            var testTasks = await _mediator.Send(
               new GetEntitiesByForeignKeyQuery<TestTask>(tt => tt.VacancyId == vacancyId),
               cancellationToken);
            var testTasksIds = testTasks.Select(tt => tt.Id);
            var files = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<RecruitingStaffWebAppFile>(
                    tt => testTasksIds.Contains(tt.TestTaskId == null ? 0 : tt.TestTaskId.Value)),
                cancellationToken);
            foreach (var file in files)
            {
                await _mediator.Send(new RemoveFileCommand(file.Id), cancellationToken);
            }
            foreach (var testTask in testTasks)
            {
                _ = await _mediator.Send(new RemoveEntityCommand<TestTask>(testTask.Id), cancellationToken);
            }
        }
    }
}
