using MediatR;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Feature.Task.Queries
{
    public record GetPagedTasksQuery(int PageNumber, int PageSize) : IRequest<Result<Pagination<TaskItem>>>;

    public class GetPagedTasksHandler : IRequestHandler<GetPagedTasksQuery, Result<Pagination<TaskItem>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPagedTasksHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Result<Pagination<TaskItem>>> Handle(GetPagedTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _unitOfWork.Tasks.GetPagedAsync(request.PageNumber, request.PageSize);
            return Result<Pagination<TaskItem>>.Success(tasks);
        }
    }

}
