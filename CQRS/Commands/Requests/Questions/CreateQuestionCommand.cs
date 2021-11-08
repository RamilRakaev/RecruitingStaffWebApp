using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions
{
    public class CreateQuestionCommand : IRequest<bool>
    {
        public CreateQuestionCommand(Question question)
        {
            Question = question;
        }

        public Question Question { get; set; }
    }
}
