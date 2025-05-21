namespace Services.Contracts
{
    public interface IServiceManager
    {
        IUserService User { get; }
        ITaskService Task { get; }
        ITeamService Team { get; }
        IAuthenticationService Authentication { get; }
    }
}
