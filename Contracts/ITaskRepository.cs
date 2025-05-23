using Entities.Models;

namespace Contracts
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem item);
        Task AddRangeAsync(List<TaskItem> entities);
        void Deprecate(TaskItem task);
        void Edit(TaskItem task);
        Task<List<TaskItem>> FindAsync(Guid teamId);
        Task<TaskItem?> FindByIdAsync(Guid id, bool track);
        IQueryable<TaskItem> Query(Guid teamId, bool include = false);
    }
}
