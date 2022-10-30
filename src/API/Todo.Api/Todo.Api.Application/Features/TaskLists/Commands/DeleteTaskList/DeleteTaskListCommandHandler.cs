using MediatR;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Application.Exceptions;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Features.TaskLists.Commands.DeleteTaskList
{
    public class DeleteTaskListCommandHandler : IRequestHandler<DeleteTaskListCommand>
    {
        private readonly IAsyncRepository<Entities.TaskList> _taskListRepository;

        public DeleteTaskListCommandHandler(IAsyncRepository<Entities.TaskList> taskListRepository)
        {
            _taskListRepository = taskListRepository;
        }

        public async Task<Unit> Handle(DeleteTaskListCommand request, CancellationToken cancellationToken)
        {
            var taskListToDelete = await _taskListRepository.GetByIdAsync(request.Id);

            if (taskListToDelete == null)
            {
                throw new NotFoundException(nameof(Entities.TaskList), request.Id);
            }

            await _taskListRepository.DeleteAsync(taskListToDelete);

            return Unit.Value;
        }
    }
}