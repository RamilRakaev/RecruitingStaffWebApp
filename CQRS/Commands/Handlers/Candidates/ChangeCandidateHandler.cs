using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{

    public class ChangeCandidateHandler : CandidateFileManagement, IRequestHandler<ChangeCandidateCommand, bool>
    {
        private readonly IRepository<Candidate> _candidateRepository;

        public ChangeCandidateHandler(IRepository<Candidate> candidateRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost) : base(fileRepository, options, webHost)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> Handle(ChangeCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.FindAsync(request.Candidate.Id, cancellationToken);
            candidate.FullName = request.Candidate.FullName;
            candidate.DateOfBirth = request.Candidate.DateOfBirth;
            candidate.Address = request.Candidate.Address;
            candidate.TelephoneNumber = request.Candidate.TelephoneNumber;
            candidate.MaritalStatus = request.Candidate.MaritalStatus;
            await RewritePhoto(request.UploadedFile, candidate, cancellationToken);
            await _candidateRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
