using System.Threading.Tasks;

namespace TestLocker.Services
{
    public interface IEmailService
    {
        Task<int> SendEmail(string to, string subject, string body);
    }
}
