using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<ITaskService> _taskService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IAppLogger logger, IRepositoryManager repository, UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userService = new Lazy<IUserService>(() =>
                new UserService(logger));
            _taskService = new Lazy<ITaskService>(() =>
                new TaskService(repository, logger));
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                new AuthenticationService(logger, userManager, configuration));
        }

        public IUserService User => _userService.Value;
        public ITaskService Task => _taskService.Value;
        public IAuthenticationService Authentication => _authenticationService.Value;
    }
}
