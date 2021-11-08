using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers
{
    public class ChangeAnswerCommand : IRequest<bool>
    {
        public ChangeAnswerCommand(Answer answer)
        {
            Answer = answer;
        }

        public Answer Answer { get; set; }
    }
}
