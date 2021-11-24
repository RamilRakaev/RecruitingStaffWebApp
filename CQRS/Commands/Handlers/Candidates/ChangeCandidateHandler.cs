using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.FileManagement;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class ChangeCandidateHandler : CandidateDocumentsManagement, IRequestHandler<ChangeCandidateCommand, bool>
    {
        private protected IRepository<Candidate> _candidateRepository;
        private protected IRepository<Questionnaire> _questionnaireRepository;

        public ChangeCandidateHandler(
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

        public async Task<bool> Handle(ChangeCandidateCommand request, CancellationToken cancellationToken)
        {
            await _candidateRepository.Update(request.Candidate);
            if (request.UploadedFile != null)
            {
                await DeleteCandidateFiles(request.Candidate.Id, cancellationToken);
                var candidate = await _candidateRepository.FindNoTrackingAsync(request.Candidate.Id, cancellationToken);
                var questionnaire = await _questionnaireRepository.FindNoTrackingAsync(request.QuestionnaireId, cancellationToken);
                await SaveDocument(
                    request.UploadedFile,
                    candidate,
                    questionnaire.Name,
                    cancellationToken);

            }
            await _candidateRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
