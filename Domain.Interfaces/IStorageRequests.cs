using RecruitingStaff.Domain.Model;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaff.Domain.Interfaces
{
    public interface IStorageRequests<Entity> where Entity : BaseEntity
    {
        public Task<Entity> FindNoTrackingAsync(int id);

        public Task<Entity> FindAsync(int id);

        public IQueryable<Entity> GetAll();

        public IQueryable<Entity> GetAllAsNoTracking();
    }
}
