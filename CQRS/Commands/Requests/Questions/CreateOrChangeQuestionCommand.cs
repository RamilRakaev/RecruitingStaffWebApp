using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions
{
    public class CreateOrChangeQuestionCommand : IRequest<bool>
    {
        public CreateOrChangeQuestionCommand(Question question)
        {
            Question = question;
        }

        public Question Question { get; set; }
    }
}
