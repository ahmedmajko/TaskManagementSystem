using MediatR;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Application.Feature.Task.Commands
{
    public record DeleteTaskCommand(int Id) : IRequest<Result<Unit>>;

    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTaskHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Result<Unit>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id);
            if (task == null) return Result<Unit>.Failure("Task not found");

            _unitOfWork.Tasks.Remove(task);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }

}
