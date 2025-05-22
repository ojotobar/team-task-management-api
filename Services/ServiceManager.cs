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
        private readonly Lazy<ITeamService> _teamService;

        public ServiceManager(IAppLogger logger, IRepositoryManager repository, UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userService = new Lazy<IUserService>(() =>
                new UserService(logger, repository, userManager));
            _taskService = new Lazy<ITaskService>(() =>
                new TaskService(repository, logger, userManager));
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                new AuthenticationService(logger, userManager, configuration));
            _teamService = new Lazy<ITeamService>(() =>
                new TeamService(logger, repository, userManager));
        }

        public IUserService User => _userService.Value;
        public ITaskService Task => _taskService.Value;
        public IAuthenticationService Authentication => _authenticationService.Value;
        public ITeamService Team => _teamService.Value;
    }
}
