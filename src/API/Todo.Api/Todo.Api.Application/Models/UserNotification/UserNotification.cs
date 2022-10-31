namespace Todo.Api.Application.Models.UserNotification
{
    public class UserNotification
    {

        public string UserId { get; set; }

        public int TaskCompletedCount { get; set; }

        public UserNotification(int taskCompletedCount, string userId)
        {
            TaskCompletedCount = taskCompletedCount;
            UserId = userId;
        }

        public UserNotification()
        {

        }
    }
}
