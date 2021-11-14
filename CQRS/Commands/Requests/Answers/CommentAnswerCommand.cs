using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers
{
    public class CommentAnswerCommand : IRequest<bool>
    {
        public CommentAnswerCommand()
        {

        }

        public CommentAnswerCommand(int aswerId, int candidateId)
        {
            AnswerId = aswerId;
            CandidateId = candidateId;
        }

        public int AnswerId { get; set; }
        public int CandidateId { get; set; }
        public string Comment { get; set; }
    }
}
