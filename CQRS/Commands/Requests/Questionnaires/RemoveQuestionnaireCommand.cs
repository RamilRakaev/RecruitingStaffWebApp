using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class RemoveQuestionnaireCommand : IRequest<bool>
    {
        public RemoveQuestionnaireCommand(int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
        }

        public int QuestionnaireId { get; set; }
    }
}
