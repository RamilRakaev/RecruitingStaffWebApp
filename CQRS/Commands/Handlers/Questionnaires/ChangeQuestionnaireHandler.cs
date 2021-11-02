using MediatR;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Questionnaires
{
    public class ChangeQuestionnaireHandler : IRequestHandler<ChangeQuestionnaireCommand, bool>
    {
        private readonly IRepository<Questionnaire> _questionnaireReepository;

        public ChangeQuestionnaireHandler(IRepository<Questionnaire> questionnaireReepository)
        {
            _questionnaireReepository = questionnaireReepository;
        }

        public async Task<bool> Handle(ChangeQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionnaireReepository
                .FindAsync(request.Questionnaire.Id);
            questionnaire.Name = request.Questionnaire.Name;
            await _questionnaireReepository.SaveAsync();
            return true;
        }
    }
}
