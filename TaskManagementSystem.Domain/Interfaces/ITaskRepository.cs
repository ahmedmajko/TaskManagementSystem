using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);
        void Remove(TaskItem task);
        Task<TaskItem?> GetByIdAsync(int id);
        Task<Pagination<TaskItem>> GetPagedAsync(int pageNumber, int pageSize);
    }

}
