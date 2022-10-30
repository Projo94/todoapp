using AutoMapper;
using MediatR;
using Todo.Api.Application.Features.Queries.GetTaskList;
using Enities = Todo.Api.Domain.Entities;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Application.Models.Filters;

namespace Todo.Api.Application.Features.TaskLists.Queries.GetTaskList
{
    public class GetTaskListQueryHandler : IRequestHandler<GetTaskListQuery, List<TaskListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskListRepository _taskListRepository;

        public GetTaskListQueryHandler(IMapper mapper, ITaskListRepository taskListRepository)
        {
            _mapper = mapper;
            _taskListRepository = taskListRepository;
        }

        public async Task<List<TaskListVm>> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<ListFilterDto>(request.filter);

            var taskLists = await _taskListRepository.GetItemsAsync(filter);

            var results = _mapper.Map<IEnumerable<Enities.TaskList>, IEnumerable<TaskListVm>>(taskLists).ToList();

            foreach (var item in results)
            {
                item.DailyListVm = item.DailyList.DateTime.ToString();
            }

            return results;
        }
    }
}