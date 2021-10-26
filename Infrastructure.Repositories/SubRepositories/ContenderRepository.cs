using Domain.Model;

namespace Infrastructure.Repositories.SubRepositories
{
    public class ContenderRepository : BaseRepository<Contender>
    {
        public ContenderRepository(DataContext context) : base(context)
        {
        }
    }
}
