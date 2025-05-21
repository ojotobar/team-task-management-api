using Contracts;
using Entities.Models;

namespace Repositories
{
    public sealed class TaskRepository : RepositoryBase<TaskItem>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        { }

        public async Task AddAsync(TaskItem item) =>
            await CreateAsync(item);
    }
}