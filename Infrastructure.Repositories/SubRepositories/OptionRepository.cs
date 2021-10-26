using Domain.Model;

namespace Infrastructure.Repositories.SubRepositories
{
    public class OptionRepository : BaseRepository<Option>
    {
        public OptionRepository(DataContext context) : base(context)
        {
        }
    }
}
