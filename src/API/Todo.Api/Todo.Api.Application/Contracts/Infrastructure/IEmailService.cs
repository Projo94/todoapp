using Todo.Api.Application.Models.Mail;

namespace Todo.Api.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        public void SendEmail(EmailDto request);
    }
}