using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class UploadFileHandler : CandidateFilesRewriter, IRequestHandler<UploadFileCommand, bool>
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public UploadFileHandler(
            IRepository<Candidate> candidateRepository,
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options) : base(fileRepository, options)
        {
            _candidateRepository = candidateRepository;
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<bool> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.FindAsync(request.CandidateId);
            var questionnaire = await _questionnaireRepository.FindAsync(request.QuestionnaireId);
            await SaveFile(request.UploadedFile, candidate, questionnaire);
            return true;
        }
    }
}
