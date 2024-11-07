using Core.DTOS;

namespace Task_Management_System_API.Services.Interfaces
{
    public interface IMaillingService
    {
        Task SendMailAsync(string mailTo, string subject, string body, IList<IFormFile> files = null);
        Task SendMailBySendGridAsync(MailRequestDTO mailRequest);
    }
}
