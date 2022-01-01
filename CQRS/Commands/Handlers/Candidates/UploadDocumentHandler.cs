using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.FileManagement;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class UploadDocumentHandler : CandidateDocumentsManagement, IRequestHandler<UploadDocumentCommand, bool>
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public UploadDocumentHandler(
            IRepository<Candidate> candidateRepository,
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost) : base(
                fileRepository,
                options,
                webHost)
        {
            _candidateRepository = candidateRepository;
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<bool> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.FindNoTrackingAsync(request.CandidateId, cancellationToken);
            var questionnaire = await _questionnaireRepository.FindNoTrackingAsync(request.QuestionnaireId, cancellationToken);
            await SaveDocument(request.UploadedFile, candidate, questionnaire.Name, cancellationToken);
            return true;
        }
    }
}
