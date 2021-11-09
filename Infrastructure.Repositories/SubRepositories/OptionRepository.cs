using RecruitingStaff.Domain.Model;

namespace RecruitingStaff.Infrastructure.Repositories.SubRepositories
{
    public class OptionRepository : BaseRepository<Option>
    {
        public OptionRepository(DataContext context) : base(context)
        { }
    }
}
