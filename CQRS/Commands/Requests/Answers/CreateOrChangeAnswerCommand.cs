using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers
{
    public class CreateOrChangeAnswerCommand : IRequest<bool>
    {
        public CreateOrChangeAnswerCommand(Answer answer)
        {
            Answer = answer;
        }

        public Answer Answer { get; set; }
    }
}
