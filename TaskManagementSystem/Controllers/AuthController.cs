using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Feature.UserManagment.Commands;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/auth")]

    public class AuthController : ApiBaseController
    {
        public AuthController(IMediator mediator) : base(mediator) { }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command) => HandleResult(await _mediator.Send(command));

    }


}
