using MediatR;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Application.Feature.Task.Commands
{
    public record UpdateTaskCommand(int Id, string Title, string Description, string Status, DateTime DueDate) : IRequest<Result<Unit>>;

    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateTaskHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Result<Unit>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id);
            if (task == null) return Result<Unit>.Failure("Task not found");

            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.DueDate = request.DueDate;

            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }

}
