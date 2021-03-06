using RecruitingStaff.Domain.Model.BaseEntities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Domain.Interfaces
{
    public interface IStorageRequests<Entity> where Entity : BaseEntity
    {
        public Task<Entity> FindNoTrackingAsync(int id, CancellationToken cancellationToken);

        public Task<Entity> FindAsync(int id, CancellationToken cancellationToken);

        public IQueryable<Entity> GetAll();

        public IQueryable<Entity> GetAllAsNoTracking();
    }
}
