using System.Security.Claims;

namespace TestLocker.Services
{
    public interface IJwtService
    {
        string GenerateJwt(string email, ClaimsIdentity identity);
    }
}