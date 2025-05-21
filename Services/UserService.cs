using Contracts;
using Services.Contracts;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IAppLogger _logger;
        private readonly IRepositoryManager _repository;

        public UserService(IAppLogger logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public string GetUserId()
        {
            return _repository.User.GetLoggedInUserId();
        }
    }
}
