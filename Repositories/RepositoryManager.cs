using Contracts;
using Microsoft.AspNetCore.Http;

namespace Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ITaskRepository> _taskRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<ITeamRepository> _teamRepository;
        private readonly Lazy<ITeamUserRepository> _teamUserRepository;

        public RepositoryManager(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _taskRepository = new Lazy<ITaskRepository>(() =>
                new TaskRepository(context));
            _userRepository = new Lazy<IUserRepository>(() =>
                new UserRepository(contextAccessor));
            _teamRepository = new Lazy<ITeamRepository>(() =>
                new TeamRepository(context));
            _teamUserRepository = new Lazy<ITeamUserRepository>(() =>
                new TeamUserRepository(context));
        }

        public ITaskRepository Task => _taskRepository.Value;
        public IUserRepository User => _userRepository.Value;
        public ITeamRepository Team => _teamRepository.Value;
        public ITeamUserRepository TeamUser => _teamUserRepository.Value;

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
