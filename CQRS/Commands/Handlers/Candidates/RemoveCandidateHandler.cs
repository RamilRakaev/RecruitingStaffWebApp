using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class RemoveCandidateHandler : IRequestHandler<RemoveCandidateCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly WebAppOptions _options;

        public RemoveCandidateHandler(
            IMediator mediator,
            IOptions<WebAppOptions> options
            ) 
        {
            _mediator = mediator;
            _options = options.Value;
        }

        public async Task<bool> Handle(RemoveCandidateCommand request, CancellationToken cancellationToken)
        {
            var answers = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<Answer>(a => a.CandidateId == request.CandidateId));
            foreach(var answer in answers)
            {
                await _mediator.Send(new RemoveEntityCommand<Answer>(answer.Id));
            }
            var files = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<RecruitingStaffWebAppFile>(
                    f => f.CandidateId == request.CandidateId));
            foreach (var file in files)
            {
                File.Delete($"{_options.CandidateDocumentsSource}\\{file.Name}");
                await _mediator.Send(new RemoveEntityCommand<RecruitingStaffWebAppFile>(file.Id));
            }
            var candidateQuestionnaires = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<CandidateQuestionnaire>(cq => cq.FirstEntityId == request.CandidateId));
            foreach(var candidateQuestionnaire in candidateQuestionnaires)
            {
                await _mediator.Send(new RemoveEntityCommand<CandidateQuestionnaire>(candidateQuestionnaire.Id));
            }
            var candidateVacancies = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<CandidateVacancy>(cq => cq.FirstEntityId == request.CandidateId));
            foreach(var candidateVacancy in candidateVacancies)
            {
                await _mediator.Send(new RemoveEntityCommand<CandidateVacancy>(candidateVacancy.Id));
            }
            await _mediator.Send(new RemoveEntityCommand<Candidate>(request.CandidateId));
            return true;
        }
    }
}
