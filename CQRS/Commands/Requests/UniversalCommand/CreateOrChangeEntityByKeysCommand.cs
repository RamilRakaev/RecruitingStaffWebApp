using MediatR;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class CreateOrChangeEntityByKeysCommand<TEntity> : IRequest<bool> where TEntity : CandidateQuestionnaireEntity
    {
        public CreateOrChangeEntityByKeysCommand(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; set; }
    }
}
