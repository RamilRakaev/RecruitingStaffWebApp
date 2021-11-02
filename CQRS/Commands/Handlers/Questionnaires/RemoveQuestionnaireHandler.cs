using MediatR;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class RemoveQuestionnaireHandler : CandidateFilesRewriter, IRequestHandler<RemoveQuestionnaireCommand, bool>
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;

        public RemoveQuestionnaireHandler(
            IRepository<Questionnaire> questionnaireRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options) : base(fileRepository, options)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<bool> Handle(RemoveQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionnaireRepository.FindAsync(request.QuestionnaireId);
            await _questionnaireRepository.RemoveAsync(questionnaire);
            await DeleteFile(questionnaire.DocumentFile, cancellationToken);
            await _questionnaireRepository.SaveAsync();
            return true;
        }
    }
}
