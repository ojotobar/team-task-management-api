using Contracts;
using Entities.Responses;
using Services.Contracts;
using Shared;
using Shared.DTO;

namespace Services
{
    public sealed class TeamService : ITeamService
    {
        private readonly IAppLogger _logger;
        private readonly IRepositoryManager _repository;

        public TeamService(IAppLogger logger, IRepositoryManager repository)
        {
            this._logger = logger;
            this._repository = repository;
        }

        public async Task<ApiResponseBase> Create(CreateTeamDto dto)
        {
            var team = dto.Map();
            await _repository.Team.AddAsync(team);
            await _repository.SaveAsync();

            return new OkResponse<string>(ResponseMessages.TeamCreated);
        }
    }
}
