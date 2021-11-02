using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using Microsoft.Extensions.Options;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    
    public class ChangeCandidateHandler : CandidateFilesRewriter, IRequestHandler<ChangeCandidateCommand, bool>
    {
        private readonly IRepository<Candidate> _candidateRepository;

        public ChangeCandidateHandler(IRepository<Candidate> candidateRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options) : base(fileRepository, options)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> Handle(ChangeCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.FindAsync(request.Candidate.Id);
            candidate.FullName = request.Candidate.FullName;
            candidate.DateOfBirth = request.Candidate.DateOfBirth;
            candidate.Address = request.Candidate.Address;
            candidate.TelephoneNumber = request.Candidate.TelephoneNumber;
            candidate.MaritalStatus = request.Candidate.MaritalStatus;
            await RewritePhoto(request.UploadedFile, candidate, cancellationToken);
            await _candidateRepository.SaveAsync();
            return true;
        }
    }
}
