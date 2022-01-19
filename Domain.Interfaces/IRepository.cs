using RecruitingStaff.Domain.Model.BaseEntities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Domain.Interfaces
{
    public interface IRepository<Entity> : IStorageRequests<Entity> where Entity : BaseEntity
    {
        Task Update(Entity entity);

        public Task AddAsync(Entity entity, CancellationToken cancellationToken);

        public Task RemoveAsync(Entity entity);

        public Task RemoveRangeAsync(IEnumerable<Entity> entities)
        {
            throw new Exception("Method is not overridden in child class");
        }

        public Task SaveAsync(CancellationToken cancellationToken);
    }
}
