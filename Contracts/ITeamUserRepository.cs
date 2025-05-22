using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface ITeamUserRepository
    {
        Task AddRangeAsync(List<TeamUser> teamUsers);
        IQueryable<TeamUser> FindBy(Expression<Func<TeamUser, bool>> predicate);
    }
}
