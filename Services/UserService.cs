using Contracts;
using Services.Contracts;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IAppLogger _logger;

        public UserService(IAppLogger logger)
        {
            _logger = logger;
        }
    }
}
