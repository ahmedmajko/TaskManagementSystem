using MediatR;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Feature.Task.Queries
{
    public record GetTaskByIdQuery(int Id) : IRequest<Result<TaskItem>>;

    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskItem>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTaskByIdHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Result<TaskItem>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id);
            return task != null ? Result<TaskItem>.Success(task) : Result<TaskItem>.Failure("Task not found");
        }
    }

}
