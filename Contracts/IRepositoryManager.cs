namespace Contracts
{
    public interface IRepositoryManager
    {
        ITaskRepository Task { get; }
        Task SaveAsync();
    }
}
