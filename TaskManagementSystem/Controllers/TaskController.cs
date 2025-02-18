using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Feature.Task.Commands;
using TaskManagementSystem.Application.Feature.Task.Queries;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TaskController : ApiBaseController
    {
        public TaskController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => HandleResult(await _mediator.Send(new GetTaskByIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskCommand command) => HandleResult(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskCommand command) => HandleResult(await _mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => HandleResult(await _mediator.Send(new DeleteTaskCommand(id)));

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetPagedTasksQuery query) => HandleResult(await _mediator.Send(query));
    }


}
