using MyStore.Service.Models;

namespace MyStore.Service.Services.Email
{
    public interface IEmailService
    {
        Task<string> SendEmail(Message message);
    }
}
