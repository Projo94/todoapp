using AutoMapper;
using Moq;
using Shouldly;

using Todo.Api.Application.Features.TaskLists.Commands.CreateTaskList;
using Todo.Api.Domain.Entities;
using Todo.Api.Application.Profiles;
using Todo.Api.UnitTests.Mocks;
using Xunit;
using Todo.Api.Application.Contracts.Persistence;

namespace Todo.Api.UnitTests.TaskLists
{
    public class CreateTaskListTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<TaskList>> _mockTaskListRepository;

        public CreateTaskListTests()
        {
            _mockTaskListRepository = RepositoryMocks.GetTaskListRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async System.Threading.Tasks.Task Handle_ValidTaskList_AddedTaskListsRepo()
        {
            var handler = new CreateTaskListCommandHandler(_mapper, _mockTaskListRepository.Object);

            await handler.Handle(new CreateTaskListCommand()
            {
                Title = "Task list 1",
                Description = "Description of Task list 1",
                DailyList = DateTimeOffset.Now,
                CreatedByUserId = "1",
                TimeZone = "UTC-09"
            }, CancellationToken.None);

            var allTaskLists = await _mockTaskListRepository.Object.ListAllAsync();
            allTaskLists.Count.ShouldBe(4);
        }
    }
}
