using System.Threading.Tasks;
using TestLocker.Models;

namespace TestLocker.Services
{
    public interface ICurrentUserHelper
    {
        string GetCurrentUserEmail();
        Task<AppUser> GetCurrentUser();
    }
}
