using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class ChangeCandidateHandler : IRequestHandler<ChangeCandidateCommand, bool>
    {
        private readonly IRepository<Candidate> _CandidateRepository;
        private readonly IRepository<Option> _optionRepository;

        public ChangeCandidateHandler(IRepository<Candidate> CandidateRepository, IRepository<Option> optionRepository)
        {
            _CandidateRepository = CandidateRepository;
            _optionRepository = optionRepository;
        }

        public async Task<bool> Handle(ChangeCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _CandidateRepository.FindAsync(request.Candidate.Id);
            var documentSource = await _optionRepository
                .GetAllAsNoTracking()
                .FirstOrDefaultAsync(o => o.PropertyName == OptionTypes.DocumentsSource, cancellationToken: cancellationToken);
            if(documentSource != null)
            {
                var file = new FileInfo($"{documentSource.Value}\\{candidate.DocumentSource}");

                candidate.FullName = request.Candidate.FullName;
                candidate.Address = request.Candidate.Address;
                candidate.DateOfBirth = request.Candidate.DateOfBirth;
                await _CandidateRepository.SaveAsync();

                if (file.Exists)
                {
                    file.MoveTo($"{documentSource.Value}\\{candidate.DocumentSource}");
                }
                return true;
            }
            return false;
        }
    }
}
