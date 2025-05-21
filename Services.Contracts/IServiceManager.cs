namespace Services.Contracts
{
    public interface IServiceManager
    {
        IUserService User { get; }
        ITaskService Task { get; }
        IAuthenticationService Authentication { get; }
    }
}
