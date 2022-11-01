using AutoMapper;
using MediatR;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Application.Exceptions;
using Todo.Api.Application.Features.TaskList.Queries.GetTaskList;
using Todo.Api.Application.Features.Tasks.Commands.CreateTask;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Features.Task.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskCommandResponse>
    {

        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskListRepository _taskListRepository;

        public CreateTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository, ITaskListRepository taskListRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _taskListRepository = taskListRepository;
        }

        public async Task<CreateTaskCommandResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var createTaskCommandResponse = new CreateTaskCommandResponse();

            var validator = new CreateTaskCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createTaskCommandResponse.Success = false;
                createTaskCommandResponse.ValidationErrors = new List<string>();

                foreach (var error in validationResult.Errors)
                {
                    createTaskCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var task = _mapper.Map<Entities.Task>(request);

                var taskList = await _taskListRepository.GetByIdAsync(task.TaskListId);

                if (taskList == null)
                {
                    throw new NotFoundException(nameof(Entities.TaskList), request.TaskListId);
                }

                task = await _taskRepository.AddAsync(task);

                await _taskRepository.SaveChangesAsync();

                createTaskCommandResponse.Task = _mapper.Map<TaskDto>(task);

                var timezone = taskList.TimeZone;

                var deadline = task.Deadline;

                createTaskCommandResponse.Task.DeadlineVm = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(deadline, timezone).DateTime.ToString();
            }

            return createTaskCommandResponse;
        }
    }
}