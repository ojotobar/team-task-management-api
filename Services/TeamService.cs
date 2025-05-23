using Contracts;
using Entities.Models;
using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Services.Validations;
using Shared;
using Shared.DTO;

namespace Services
{
    public sealed class TeamService : ITeamService
    {
        private readonly IAppLogger _logger;
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;

        public TeamService(IAppLogger logger, IRepositoryManager repository, UserManager<User> userManager)
        {
            _logger = logger;
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<ApiResponseBase> Create(CreateTeamDto dto)
        {
            var validator = new TeamValidator().Validate(dto);
            if (!validator.IsValid)
            {
                return new BadRequestResponse(validator.Errors.FirstOrDefault()?.ErrorMessage ?? ResponseMessages.InvalidInput);
            }

            var team = dto.Map();
            await _repository.Team.AddAsync(team);
            await _repository.SaveAsync();

            return new OkResponse<TeamToReturnDto>(team.Map());
        }

        public async Task<ApiResponseBase> InviteUsers(Guid teamId, TeamInvitaionDto dto)
        {
            var validator = new TeamInvitationValidator().Validate(dto);
            if (!validator.IsValid)
            {
                return new BadRequestResponse(validator.Errors.FirstOrDefault()?.ErrorMessage ?? ResponseMessages.InvalidInput);
            }

            var team = await _repository.Team.FindByIdAsync(teamId, false);
            if (team == null)
            {
                _logger.LogError($"TeamId {teamId}: {ResponseMessages.TeamRecordNotFound}");
                return new NotFoundResponse(ResponseMessages.TeamRecordNotFound);
            }

            var users = await _userManager.Users.Where(u => dto.UserIds.Contains(u.Id))
                .ToListAsync() ?? new List<User>();

            if (!users.Count.Equals(dto.UserIds.Count))
            {
                return new BadRequestResponse(ResponseMessages.InvalidInput);
            }

            var existingUsers = await _repository.TeamUser
                .FindBy(tu => tu.TeamId.Equals(teamId) && users.Select(u => u.Id).Contains(tu.UserId))
                .ToListAsync();

            if(existingUsers.Count > 0)
            {
                _logger.LogWarn($"User(s): {string.Join(',', existingUsers.Select(u => u.Id))}, are already in the {team.Name} team.");
                return new BadRequestResponse(string.Format(ResponseMessages.UsersAlreadyInTeam, existingUsers.Count));
            }

            var teamUsers = teamId.Map(users);
            await _repository.TeamUser.AddRangeAsync(teamUsers);
            await _repository.SaveAsync();

            return new OkResponse<string>(string.Format(ResponseMessages.UsersInvited, users.Count, team.Name));
        }
    }
}
