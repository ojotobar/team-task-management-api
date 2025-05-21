using Entities.Models;

namespace Contracts
{
    public interface ITeamRepository
    {
        Task AddAsync(Team team);
    }
}
