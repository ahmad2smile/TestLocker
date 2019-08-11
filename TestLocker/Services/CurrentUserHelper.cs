using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using TestLocker.Models;

namespace TestLocker.Services
{
    public class CurrentUserHelper : ICurrentUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public CurrentUserHelper(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string GetCurrentUserEmail()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            return email;
        }

        public async Task<AppUser> GetCurrentUser()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }
    }
}
