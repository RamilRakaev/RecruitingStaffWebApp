using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions
{
    public class ChangeQuestionCommand : IRequest<bool>
    {
        public ChangeQuestionCommand(Question question)
        {
            Question = question;
        }

        public Question Question { get; set; }
    }
}
