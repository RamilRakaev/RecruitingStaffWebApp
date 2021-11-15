using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class UploadFileHandler : CandidateFileManagement, IRequestHandler<UploadFileCommand, bool>
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public UploadFileHandler(
            IRepository<Candidate> candidateRepository,
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost) : base(fileRepository, options, webHost)
        {
            _candidateRepository = candidateRepository;
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<bool> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.FindAsync(request.CandidateId, cancellationToken);
            var questionnaire = await _questionnaireRepository.FindAsync(request.QuestionnaireId, cancellationToken);
            await SaveDocument(request.UploadedFile, $"{candidate.Id}.{candidate.FullName} - {questionnaire.Name}", cancellationToken);
            return true;
        }
    }
}
