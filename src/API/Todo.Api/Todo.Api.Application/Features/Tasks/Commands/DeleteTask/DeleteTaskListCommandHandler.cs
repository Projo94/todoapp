using AutoMapper;
using MediatR;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Application.Exceptions;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Features.Task.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IAsyncRepository<Entities.Task> _taskListRepository;

        public DeleteTaskCommandHandler(IAsyncRepository<Entities.Task> taskListRepository)
        {
            _taskListRepository = taskListRepository;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var taskListToDelete = await _taskListRepository.GetByIdAsync(request.Id);

            if (taskListToDelete == null)
            {
                throw new NotFoundException(nameof(Entities.Task), request.Id);
            }

            await _taskListRepository.DeleteAsync(taskListToDelete);

            return Unit.Value;
        }
    }
}
