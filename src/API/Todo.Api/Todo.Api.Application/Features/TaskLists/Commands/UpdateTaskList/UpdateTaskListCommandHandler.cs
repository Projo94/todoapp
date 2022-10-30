using AutoMapper;
using MediatR;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Application.Exceptions;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Features.TaskLists.Commands.UpdateTaskList
{
    public class UpdateTaskListCommandHandler : IRequestHandler<UpdateTaskListCommand>
    {
        private readonly IAsyncRepository<Entities.TaskList> _taskListRepository;
        private readonly IMapper _mapper;

        public UpdateTaskListCommandHandler(IAsyncRepository<Entities.TaskList> taskListRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskListRepository = taskListRepository;
        }

        public async Task<Unit> Handle(UpdateTaskListCommand request, CancellationToken cancellationToken)
        {
            var taskListToUpdate = await _taskListRepository.GetByIdAsync(request.Id);

            if (taskListToUpdate == null)
            {
                throw new NotFoundException(nameof(Entities.TaskList), request.Id);
            }

            var validator = new UpdateTaskListCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, taskListToUpdate, typeof(UpdateTaskListCommand), typeof(Entities.TaskList));

            await _taskListRepository.UpdateAsync(taskListToUpdate);

            return Unit.Value;
        }
    }
}