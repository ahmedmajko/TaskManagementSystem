using MediatR;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Feature.Task.Commands
{
    public record CreateTaskCommand(string Title, string Description, string Status, DateTime DueDate) : IRequest<Result<int>>;

    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateTaskHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                DueDate = request.DueDate
            };

            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();
            return Result<int>.Success(task.Id);
        }
    }


}
