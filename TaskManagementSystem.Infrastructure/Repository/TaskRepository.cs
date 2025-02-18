using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Context;

namespace TaskManagementSystem.Infrastructure.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
        }
        public void Remove(TaskItem task)
        {
            _context.Tasks.Remove(task);
        }
        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<Pagination<TaskItem>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Tasks.CountAsync();
            var tasks = await _context.Tasks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Pagination<TaskItem>(tasks, totalRecords, pageNumber, pageSize);
        }
    }


}
