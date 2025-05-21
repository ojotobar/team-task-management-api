using Contracts;
using Services.Contracts;

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
    }
}
