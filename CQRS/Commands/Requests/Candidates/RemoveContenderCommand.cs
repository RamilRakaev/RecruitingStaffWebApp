using Domain.Model;
using MediatR;

namespace CQRS.Commands.Requests.Candidates
{
    public class RemoveCandidateCommand : IRequest<bool>
    {
        public RemoveCandidateCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
