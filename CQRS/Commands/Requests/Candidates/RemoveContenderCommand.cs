using RecruitingStaff.Domain.Model;
using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates
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
