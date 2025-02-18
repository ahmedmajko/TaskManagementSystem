using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected ApiBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Handles API responses using the Result pattern.
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="result">Result object</param>
        /// <returns>Appropriate HTTP response</returns>
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
