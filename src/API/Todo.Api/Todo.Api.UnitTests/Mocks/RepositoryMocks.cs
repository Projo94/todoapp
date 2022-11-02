using Moq;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Domain.Entities;

namespace Todo.Api.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IAsyncRepository<TaskList>> GetTaskListRepository()
        {
            var taskLists = new List<TaskList>
            {
                new TaskList
                {
                     Id = 1,
                     Title = "Task list 1",
                     Description = "Description of Task list 1",
                     DailyList = DateTimeOffset.Now,
                     CreatedByUserId = "1",
                     TimeZone = "UTC-09"
                },
                new TaskList
                {
                     Id = 2,
                     Title = "Task list 2",
                     Description = "Description of Task list 2",
                     DailyList = DateTimeOffset.Now,
                     CreatedByUserId = "1",
                     TimeZone = "UTC-09"
                },
                new TaskList
                {
                     Id = 3,
                     Title = "Task list 1",
                     Description = "Description of Task list 1",
                     DailyList = DateTimeOffset.Now,
                     CreatedByUserId = "1",
                     TimeZone = "UTC-09"
                },

            };

            var mockTaskListRepository = new Mock<IAsyncRepository<TaskList>>();
            mockTaskListRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(taskLists);

            mockTaskListRepository.Setup(repo => repo.AddAsync(It.IsAny<TaskList>())).ReturnsAsync(
                (TaskList taskList) =>
                {
                    taskLists.Add(taskList);
                    return taskList;
                });

            return mockTaskListRepository;
        }
    }
}
