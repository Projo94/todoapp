using AutoMapper;
using MediatR;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Application.Features.TaskList.Queries.GetTaskList;
using Enities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Features.TaskLists.Commands.CreateTaskList
{
    public class CreateTaskListCommandHandler : IRequestHandler<CreateTaskListCommand, CreateTaskListCommandResponse>
    {
        private readonly IMapper _mapper;

        private readonly ITaskListRepository _taskListRepository;

        public CreateTaskListCommandHandler(IMapper mapper, ITaskListRepository taskListRepository)
        {
            _mapper = mapper;
            _taskListRepository = taskListRepository;
        }

        public async Task<CreateTaskListCommandResponse> Handle(CreateTaskListCommand request, CancellationToken cancellationToken)
        {
            var createTaskListCommandResponse = new CreateTaskListCommandResponse();

            var validator = new CreateTaskListCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createTaskListCommandResponse.Success = false;
                createTaskListCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createTaskListCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var taskList = _mapper.Map<Enities.TaskList>(request);

                taskList = await _taskListRepository.AddAsync(taskList);

                await _taskListRepository.SaveChangesAsync();

                createTaskListCommandResponse.TaskList = _mapper.Map<TaskListDto>(taskList);

                var timezone = createTaskListCommandResponse.TaskList.Timezone;

                var dailyList = createTaskListCommandResponse.TaskList.DailyList;

                createTaskListCommandResponse.TaskList.DailyListVm = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dailyList, timezone).DateTime.ToString();
            }

            return createTaskListCommandResponse;
        }
    }
}