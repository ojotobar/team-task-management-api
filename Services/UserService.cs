using Contracts;
using Entities.Models;
using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

            var userTeams = await _repository.TeamUser.FindBy(tu => tu.UserId.Equals(userId))
                .Include(tu => tu.Team)
                .Select(t => t.Team)
                .ToListAsync() ?? new List<Team?>();

            return new OkResponse<UserWithTeamsToReturnDto>(user.Map(userTeams));
        }

        public async Task<ApiResponseBase> UserIsATeamMember(Guid teamId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError($"No user found with Id: {userId}");
                return new NotFoundResponse(ResponseMessages.UserNotFound);
            }

            var team = await _repository.Team.FindByIdAsync(teamId, false);
            if (team == null)
            {
                _logger.LogError($"No user found with Id: {teamId}");
                return new NotFoundResponse(ResponseMessages.TeamRecordNotFound);
            }

            var teamUser = await _repository.TeamUser.FindBy(tu => tu.TeamId.Equals(teamId) && tu.UserId.Equals(user.Id))
                .FirstOrDefaultAsync();

            if (teamUser == null)
            {
                return new NotFoundResponse(ResponseMessages.UserNotFound);
            }

            return new OkResponse<bool>(true);
        }

        public async Task<ApiResponseBase> GetUsers()
        {
            var users = await _userManager.Users.Where(u => u.EmailConfirmed).ToListAsync() ?? new List<User>();
            
            return new OkResponse<List<UserToReturnDto>>(users.Map());
        }
    }
}
