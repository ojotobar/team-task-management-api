using Contracts;

namespace Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ITaskRepository> _taskRepository;

        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _taskRepository = new Lazy<ITaskRepository>(() =>
                new TaskRepository(context));
        }

        public ITaskRepository Task => _taskRepository.Value;

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
