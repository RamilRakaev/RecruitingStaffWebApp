using CQRS.Commands.Requests.Candidates;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commands.Handlers.Candidates
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
            var Candidate = await _CandidateRepository.FindAsync(request.Candidate.Id);
            var documentSource = await _optionRepository
                .GetAllAsNoTracking()
                .FirstOrDefaultAsync(o => o.PropertyName == OptionTypes.DocumentsSource, cancellationToken: cancellationToken);
            if(documentSource != null)
            {
                var file = new FileInfo($"{documentSource.Value}\\{Candidate.DocumentSource}");

                Candidate.FullName = request.Candidate.FullName;
                Candidate.Address = request.Candidate.Address;
                Candidate.DateOfBirth = request.Candidate.DateOfBirth;
                await _CandidateRepository.SaveAsync();

                if (file.Exists)
                {
                    file.MoveTo($"{documentSource.Value}\\{Candidate.DocumentSource}");
                }
                return true;
            }
            return false;
        }
    }
}
