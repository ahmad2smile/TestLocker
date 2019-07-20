using System.Security.Claims;

namespace TestLocker.Services
{
    public interface IJwtService
    {
        string GenerateJwtAsync(string email, ClaimsIdentity identity);
    }
}