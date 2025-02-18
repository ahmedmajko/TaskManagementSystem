using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Feature.UserManagment.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<Result<string>>;

    public class LoginHandler : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public LoginHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Result<string>.Failure("Invalid email or password");

            var token = _jwtService.GenerateToken(user);
            return Result<string>.Success(token);
        }
    }

}
