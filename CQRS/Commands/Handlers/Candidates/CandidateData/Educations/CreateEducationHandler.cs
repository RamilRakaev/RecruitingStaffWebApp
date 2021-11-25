using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.Educations;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates.CandidateData.Educations
{
    public class CreateEducationHandler : IRequestHandler<CreateEducationCommand, Education>
    {
        private readonly IRepository<Education> _educationRepository;

        public CreateEducationHandler(IRepository<Education> educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task<Education> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            await _educationRepository.AddAsync(request.Education, cancellationToken);
            await _educationRepository.SaveAsync(cancellationToken);
            return request.Education;
        }
    }
}
