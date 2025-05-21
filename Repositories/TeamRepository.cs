using Contracts;
using Entities.Models;

namespace Repositories
{
    public sealed class TeamRepository : RepositoryBase<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext context) : base(context)
        { }
    }
}
