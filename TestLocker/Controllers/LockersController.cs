using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestLocker.Data;
using TestLocker.Models;
using TestLocker.Services;
using TestLocker.Utils;
using TestLocker.ViewModels;

namespace TestLocker.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LockersController : ApiResponse
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;

        public LockersController(ApplicationContext applicationContext, IEmailService emailService, UserManager<AppUser> userManager)
        {
            _applicationContext = applicationContext;
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var lockers = await _applicationContext.Lockers.ToListAsync();

            return Ok(lockers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(string id)
        {
            try
            {
                var locker = await _applicationContext.Lockers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(l => l.Id.ToString() == id);

                if (locker.AccessTime != null)
                {
                    return Ok(locker);
                }

                locker.AccessTime = DateTime.UtcNow;

                _applicationContext.Lockers.Update(locker);

                await _applicationContext.SaveChangesAsync();

                var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                var responseSendEmail = await _emailService.SendEmail("ahmad@oozou.com", "Verify Email", $"User {email} access locker: {locker.Id} at time: {locker.AccessTime}");

                if (responseSendEmail >= 400)
                {
                    return BadRequest(new { error = "Something went wrong please try again" });
                }

                return Ok(locker);
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Unable to fetch Locker details" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]LockerViewModel lockerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                var user = await _userManager.FindByEmailAsync(email);

                var locker = new Locker()
                {
                    Name = lockerModel.Name,
                    AllowedTime = lockerModel.AllowedTime,
                    OwnerId = user.Id,
                    Link = lockerModel.Link,
                    AccessTime = null,
                    SubmitTime = null
                };

                var entity = await _applicationContext.Lockers.AddAsync(locker);
                var createdLocker = entity.Entity;

                await _applicationContext.SaveChangesAsync();

                createdLocker.Owner = null; // TODO: Json ignore not working

                return Ok(createdLocker);
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Unable to create the Locker" });
            }
        }
    }
}