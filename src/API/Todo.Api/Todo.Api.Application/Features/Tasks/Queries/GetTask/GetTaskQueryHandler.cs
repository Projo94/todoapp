using AutoMapper;
using MediatR;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Application.Models.Filters;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Features.Task.Queries.GetTask
{
    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, List<TaskVm>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskListRepository _taskListRepository;

        public GetTaskQueryHandler(IMapper mapper, ITaskRepository taskRepository, ITaskListRepository taskListRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _taskListRepository = taskListRepository;
        }

        public async Task<List<TaskVm>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<TaskFilterDto>(request.filter);

            var tasks = await _taskRepository.GetItemsAsync(filter);

            var results = _mapper.Map<IEnumerable<Entities.Task>, IEnumerable<TaskVm>>(tasks).ToList();

            foreach (var item in results)
            {
                var timezone = await _taskListRepository.GetTimeZoneAsync(item.TaskListId);

                item.DeadlineVm = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(item.Deadline, timezone).DateTime.ToString();
            }

            return results;
        }
    }
}