using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class CreateOrChangeCandidateHandler : IRequestHandler<CreateOrChangeCandidateCommand, Candidate>
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IMediator _mediator;

        public CreateOrChangeCandidateHandler(IRepository<Candidate> candidateRepository, IMediator mediator)
        {
            _candidateRepository = candidateRepository;
            _mediator = mediator;
        }

        public async Task<Candidate> Handle(CreateOrChangeCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository
                .FindNoTrackingAsync(request.Candidate.Id, cancellationToken)
                ?? await _candidateRepository
                .GetAllAsNoTracking()
                .Where(e => e.Name == request.Candidate.Name
                && e.DateOfBirth == request.Candidate.DateOfBirth)
                .FirstOrDefaultAsync(cancellationToken);

            if (candidate == null)
            {
                await _candidateRepository.AddAsync(request.Candidate, cancellationToken);
            }
            else
            {
                request.Candidate.Id = candidate.Id;
                await RemoveCandidateEntities(request.Candidate.Id);
                await CreateCandidateData(request.Candidate);
                await _candidateRepository.Update(request.Candidate);
            }
            await _candidateRepository.SaveAsync(cancellationToken);
            return request.Candidate;
        }

        private async Task CreateCandidateData(Candidate candidate)
        {
            if (candidate.Educations != null)
            {
                foreach (var education in candidate.Educations)
                {
                    education.CandidateId = candidate.Id;
                    await _mediator.Send(new CreateEntityCommand<Education>(education));
                }
            }
            if (candidate.PreviousJobs != null)
            {
                foreach (var previousJob in candidate.PreviousJobs)
                {
                    previousJob.CandidateId = candidate.Id;
                    await _mediator.Send(new CreateEntityCommand<PreviousJobPlacement>(previousJob));
                }
            }
        }

        private async Task RemoveCandidateEntities(int candidateId)
        {
            var educations =
                await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<Education>(
                    e => e.CandidateId == candidateId));
            await RemoveData(educations);
            var previousJobPlacements =
                await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<PreviousJobPlacement>(
                    e => e.CandidateId == candidateId));
            await RemoveData(previousJobPlacements);
            var previousJobPlacementIds = previousJobPlacements.Select(pjp => pjp.Id);
            var reccomenders = await _mediator.Send(
                new GetEntitiesByForeignKeyQuery<Recommender>(r => previousJobPlacementIds.Contains(r.Id)));
            await RemoveData(reccomenders);
        }

        private async Task RemoveData<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : CandidatesSelectionEntity, new()
        {
            foreach (var education in entities)
            {
                await _mediator.Send(new CreateOrChangeEntityCommand<TEntity>(education));
            }
        }
    }
}
