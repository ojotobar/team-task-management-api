using Contracts;
using Entities.Models;

namespace Repositories
{
    public sealed class TeamRepository : RepositoryBase<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext context) : base(context)
        { }

        public async Task AddAsync(Team team) =>
            await CreateAsync(team);
    }
}
