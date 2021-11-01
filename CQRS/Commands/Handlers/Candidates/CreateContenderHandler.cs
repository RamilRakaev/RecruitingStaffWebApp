using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using Domain.Interfaces;
using Domain.Model;
using Domain.Model.CandidateQuestionnaire;
using MediatR;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, bool>
    {
        private readonly IRepository<Candidate> _CandidateRepository;
        private readonly IRepository<Option> _optionRepository;

        public CreateCandidateHandler(IRepository<Candidate> CandidateRepository, IRepository<Option> optionRepository)
        {
            _CandidateRepository = CandidateRepository;
            _optionRepository = optionRepository;
        }

        public async Task<bool> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var documentSource = _optionRepository.GetAllAsNoTracking().FirstOrDefault(o => o.PropertyName == OptionTypes.DocumentsSource);
            if (documentSource != null)
            {
                await _CandidateRepository.AddAsync(request.Candidate);
                await _CandidateRepository.SaveAsync();
                var candidate = _CandidateRepository.GetAll().FirstOrDefault(c => c == request.Candidate);
                string path = $"{documentSource.Value}\\{candidate.DocumentSource}";
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await request.UploadedFile.CopyToAsync(fileStream, cancellationToken);
                }
                await _CandidateRepository.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
