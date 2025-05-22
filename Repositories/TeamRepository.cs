using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public sealed class TeamRepository : RepositoryBase<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext context) : base(context)
        { }

        public async Task AddAsync(Team team) =>
            await CreateAsync(team);

        public async Task<Team?> FindByIdAsync(Guid id, bool trachChanges) =>
            await FindOneAsync(t => t.Id.Equals(id), trachChanges);

        public async Task<List<Team>> FindAllAsync(bool trachChanges) =>
            await FindMany(trachChanges)
                .ToListAsync();
    }
}