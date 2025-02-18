using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
    }
}
