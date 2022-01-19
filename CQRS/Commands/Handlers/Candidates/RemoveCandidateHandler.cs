using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Model;
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
                new GetEntitiesByForeignKeyQuery<Answer>(a => a.CandidateId == request.CandidateId),
                cancellationToken);
            foreach(var answer in answers)
            {
                await _mediator.Send(new RemoveEntityCommand<Answer>(answer.Id),
                    cancellationToken);
            }
            var files = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<RecruitingStaffWebAppFile>(
                    f => f.CandidateId == request.CandidateId),
                cancellationToken);
            foreach (var file in files)
            {
                File.Delete($"{_options.GetSource(file.FileType)}\\{file.Name}");
                await _mediator.Send(new RemoveEntityCommand<RecruitingStaffWebAppFile>(file.Id),
                    cancellationToken);
            }
            var options = await _mediator.Send(new GetEntitiesByForeignKeyQuery<Option>(
                o => o.CandidateId == request.CandidateId),
                cancellationToken);
            foreach(var option in options)
            {
                await _mediator.Send(new RemoveEntityCommand<Option>(option.Id));
            }
            var candidateQuestionnaires = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<CandidateQuestionnaire>(cq => cq.FirstEntityId == request.CandidateId),
                cancellationToken);
            foreach(var candidateQuestionnaire in candidateQuestionnaires)
            {
                await _mediator.Send(new RemoveEntityCommand<CandidateQuestionnaire>(candidateQuestionnaire.Id),
                    cancellationToken);
            }
            var candidateVacancies = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<CandidateVacancy>(cq => cq.FirstEntityId == request.CandidateId),
                cancellationToken);
            foreach(var candidateVacancy in candidateVacancies)
            {
                await _mediator.Send(new RemoveEntityCommand<CandidateVacancy>(candidateVacancy.Id),
                    cancellationToken);
            }
            await _mediator.Send(new RemoveEntityCommand<Candidate>(request.CandidateId),
                cancellationToken);
            return true;
        }
    }
}
