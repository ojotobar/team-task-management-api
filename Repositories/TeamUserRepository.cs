using Contracts;
using Entities.Models;
using System.Linq.Expressions;

namespace Repositories
{
    public sealed class TeamUserRepository : RepositoryBase<TeamUser>, ITeamUserRepository
    {
        public TeamUserRepository(AppDbContext context): base(context)
        { }

        public async Task AddRangeAsync(List<TeamUser> teamUsers) =>
            await CreateRangeAsync(teamUsers);

        public IQueryable<TeamUser> FindBy(Expression<Func<TeamUser, bool>> predicate) =>
            FindMany(predicate);
    }
}
