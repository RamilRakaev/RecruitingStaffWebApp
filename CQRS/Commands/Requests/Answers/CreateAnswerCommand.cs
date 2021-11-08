using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers
{
    public class CreateAnswerCommand : IRequest<bool>
    {
        public CreateAnswerCommand(Answer answer)
        {
            Answer = answer;
        }

        public Answer Answer { get; set; }
    }
}
