using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers
{
    public class CommentAnswerCommand : IRequest<bool>
    {
        public CommentAnswerCommand(Answer answer)
        {
            Answer = answer;
        }

        public Answer Answer { get; set; }
        public int AnswerId { get; set; }
        public int CandidateId { get; set; }
        public string Comment { get; set; }
    }
}
