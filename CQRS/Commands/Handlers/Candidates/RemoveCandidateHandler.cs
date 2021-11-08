using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class RemoveCandidateHandler : CandidateFilesRewriter, IRequestHandler<RemoveCandidateCommand, bool>
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<Questionnaire> _questionnaiRerepository;
        private readonly IRepository<Answer> _answerRepository;

        public RemoveCandidateHandler(
            IRepository<Candidate> candidateRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IRepository<Questionnaire> questionnaiRerepository,
            IRepository<Answer> answerRepository) : base(fileRepository, options)
        {
            _candidateRepository = candidateRepository;
            _questionnaiRerepository = questionnaiRerepository;
            _answerRepository = answerRepository;
        }

        public async Task<bool> Handle(RemoveCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.FindAsync(request.Id);
            if (candidate != null)
            {
                foreach (var candidateQuestionnaire in candidate.CandidateQuestionnaires)
                {
                    var questionnaire = await _questionnaiRerepository.FindAsync(candidateQuestionnaire.Id);
                    foreach(var document in candidate.Documents)
                    {
                        await DeleteFile(document);
                    }
                    await _questionnaiRerepository.RemoveAsync(questionnaire);
                }
                File.Delete(candidate.Photo.Source);
                foreach (var candidateAnswer in candidate.Answers)
                {
                    await _answerRepository.RemoveAsync(candidateAnswer);
                }
                await _candidateRepository.RemoveAsync(candidate);
                await _candidateRepository.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
