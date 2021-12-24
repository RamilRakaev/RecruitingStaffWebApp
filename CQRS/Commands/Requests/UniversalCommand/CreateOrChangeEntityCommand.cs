using MediatR;
using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class CreateOrChangeEntityCommand<TEntity> : IRequest<TEntity>
        where TEntity : CandidateQuestionnaireEntity, new()
    {
        public CreateOrChangeEntityCommand(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; set; }
    }
}
