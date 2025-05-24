using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface ITeamRepository
    {
        Task AddAsync(Team team);
        Task<List<Team>> FindAllAsync(bool trachChanges);
        Task<Team?> FindByIdAsync(Guid id, bool trachChanges);
    }
}
