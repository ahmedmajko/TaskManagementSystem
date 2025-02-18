using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Infrastructure.Context;

namespace TaskManagementSystem.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ITaskRepository Tasks { get; }

        public UnitOfWork(ApplicationDbContext context, ITaskRepository taskRepository)
        {
            _context = context;
            Tasks = taskRepository;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }

}
