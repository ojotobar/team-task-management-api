using Contracts;
using Services.Contracts;

namespace Services
{
    public sealed class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repository;
        private readonly IAppLogger _logger;

        public TaskService(IRepositoryManager repository, IAppLogger logger)
        {
            _logger = logger;
            _repository = repository;
        }
    }
}
