using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using TestLocker.Data;
using TestLocker.Models;
using TestLocker.Services;
using TestLocker.ViewModels;

namespace TestLocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationContext _applicationContext;
        private readonly IJwtService _jwtService;

        public AuthController(UserManager<AppUser> userManager, ApplicationContext applicationContext, IJwtService jwtService)
        {
            _userManager = userManager;
            _applicationContext = applicationContext;
            _jwtService = jwtService;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] AppUserViewModel user)
        {
            var appUser = new AppUser()
            {
                Email = user.Email,
                UserName = user.Email
            };

            var userExists = await _userManager.FindByEmailAsync(user.Email);

            if (userExists != null)
            {
                return BadRequest(new { error = "User Already exists" });
            }

            var result = await _userManager.CreateAsync(appUser, user.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            _applicationContext.Users.Add(appUser);
            await _applicationContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] AppUserViewModel user)
        {
            if (!TryValidateModel(ModelState))
            {
                return BadRequest(ModelState);
            }

            var appUser = await _userManager.FindByEmailAsync(user.Email);

            if (appUser == null)
            {
                return BadRequest(ModelState);
            }

            var userVerified = await _userManager.CheckPasswordAsync(appUser, user.Password);

            if (!userVerified)
            {
                return BadRequest(ModelState);
            }

            var identity = new ClaimsIdentity(
                new GenericIdentity(appUser.Email, "Token"),
                new[] {
                    new Claim("id", appUser.Id),
                    new Claim("rol", "api_access")
                });

            var token = _jwtService.GenerateJwtAsync(user.Email, identity);

            return Ok(token);
        }
    }
}
