using Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<Entity> : IStorageRequests<Entity> where Entity : BaseEntity
    {
        public Task AddAsync(Entity entity);

        public Task RemoveAsync(int id);

        public Task RemoveRangeAsync(IEnumerable<Entity> entities)
        {
            throw new Exception("Method is not overridden in child class");
        }

        public Task SaveAsync();
    }
}
