using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Todo.Api.Application.Contracts.Infrastructure;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Identity.Models;

namespace Todo.Api.Services
{
    public class TaskListNotificationService
    {
        private readonly IMapper _mapper;
        private readonly ITaskListRepository _taskListRepo;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskListNotificationService(IMapper mapper, ITaskListRepository taskListRepo, IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _taskListRepo = taskListRepo;
            _emailService = emailService;
            _userManager = userManager;
        }

        public TaskListNotificationService()
        {
        }

        public void SendNotifications()
        {
            var notifications = _taskListRepo.GetDataForNotificationAsync();

            foreach (var item in notifications.Result)
            {
                var user = _userManager.FindByNameAsync(item.UserId).Result;

                _emailService.SendEmail(new Application.Models.Mail.EmailDto
                {
                    To = user.Email,
                    Subject = "Todo notification",
                    Body = $"Number of completed tasks today is: {item.TaskCompletedCount}"
                });
            }
        }
    }
}