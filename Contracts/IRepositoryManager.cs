namespace Contracts
{
    public interface IRepositoryManager
    {
        ITaskRepository Task { get; }
        IUserRepository User { get; }
        ITeamRepository Team { get; }
        ITeamUserRepository TeamUser { get; }
        Task SaveAsync();
    }
}
