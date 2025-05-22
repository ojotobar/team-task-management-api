using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public sealed class TaskRepository : RepositoryBase<TaskItem>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        { }

        public async Task AddAsync(TaskItem item) =>
            await CreateAsync(item);

        public async Task<List<TaskItem>> FindAsync(Guid teamId)
        {
            return await FindMany(t => t.TeamId.Equals(teamId) && !t.IsDeprecated)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public IQueryable<TaskItem> Query(Guid teamId, bool include = false)
        {
            var tasks = FindMany(t => t.TeamId.Equals(teamId) && !t.IsDeprecated);
            if (include)
            {
                tasks = tasks.Include(t => t.AssignedToUser)
                    .Include(t => t.CreatedByUser)
                    .Include(t => t.Team);
            }

            return tasks;
        }

        public async Task<TaskItem?> FindByIdAsync(Guid id, bool track)
        {
            return await FindOneAsync(t => t.Id.Equals(id) && !t.IsDeprecated, track);
        }

        public void Edit(TaskItem task) => 
            Update(task);

        public void Deprecate(TaskItem task) =>
            SoftDelete(task);
    }
}