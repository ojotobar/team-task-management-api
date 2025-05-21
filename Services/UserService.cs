using Contracts;
using Entities.Models;
using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using Shared;
using Shared.DTO;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IAppLogger _logger;
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;

        public UserService(IAppLogger logger, IRepositoryManager repository, UserManager<User> userManager)
        {
            _logger = logger;
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<ApiResponseBase> GetLoggedInUserInfo()
        {
            var userId = _repository.User.GetLoggedInUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return new BadRequestResponse(ResponseMessages.InvalidUserCredentials);
            }

            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                _logger.LogError($"No user found with Id: {userId}");
                return new NotFoundResponse(ResponseMessages.UserNotFound);
            }

            return new OkResponse<UserToReturnDto>(user.Map());
        }
    }
}
