namespace TaskManagementSystem.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        Task<int> SaveChangesAsync();
    }


}
