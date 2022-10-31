using AutoMapper;
using MediatR;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Application.Exceptions;
using Todo.Api.Application.Features.Task.Commands.UpdateTask;
using Todo.Api.Application.Features.Tasks.Commands.UpdateTask;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Features.Task.Commands.UpdateTaskList
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskToUpdate = await _taskRepository.GetByIdAsync(request.Id);

            if (taskToUpdate == null)
            {
                throw new NotFoundException(nameof(Entities.Task), request.Id);
            }

            var validator = new UpdateTaskCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, taskToUpdate, typeof(UpdateTaskCommand), typeof(Entities.TaskList));

            await _taskRepository.UpdateAsync(taskToUpdate);

            return Unit.Value;
        }
    }
}