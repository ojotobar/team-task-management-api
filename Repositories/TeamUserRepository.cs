using Contracts;
using Entities.Models;

namespace Repositories
{
    public sealed class TeamUserRepository : RepositoryBase<TeamUser>, ITeamUserRepository
    {
        public TeamUserRepository(AppDbContext context): base(context)
        { }
    }
}
