using CQRS.Commands.Requests.Candidates;
using Domain.Interfaces;
using Domain.Model;
using MediatR;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Commands.Handlers.Candidates
{
    public class RemoveCandidateHandler : IRequestHandler<RemoveCandidateCommand, bool>
    {
        private readonly IRepository<Candidate> _CandidateRepository;
        private readonly IRepository<Option> _optionRepository;

        public RemoveCandidateHandler(IRepository<Candidate> CandidateRepository, IRepository<Option> optionRepository)
        {
            _CandidateRepository = CandidateRepository;
            _optionRepository = optionRepository;
        }

        public async Task<bool> Handle(RemoveCandidateCommand request, CancellationToken cancellationToken)
        {
            var Candidate = await _CandidateRepository.FindAsync(request.Id);
            if (Candidate != null)
            {
                var documentSource = _optionRepository
                    .GetAllAsNoTracking()
                    .FirstOrDefault(
                    o => o.PropertyName == OptionTypes.DocumentsSource);

                if (documentSource != null)
                {
                    string path = documentSource.Value + "\\" + Candidate.DocumentSource;
                    File.Delete(path);
                    await _CandidateRepository.RemoveAsync(Candidate);
                    await _CandidateRepository.SaveAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
