using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestLocker.Data;
using TestLocker.Models;
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

        public LockersController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var lockers = await _applicationContext.Lockers.ToListAsync();

            return Ok(lockers);
        }

        [HttpGet(nameof(Details))]
        public async Task<ActionResult> Details(Guid id)
        {
            var locker = await _applicationContext.Lockers.FirstOrDefaultAsync(l => l.Id == id);

            return Ok(locker);
        }

        [HttpPost(nameof(Create))]
        public async Task<ActionResult> Create([FromBody]LockerViewModel lockerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locker = new Locker()
            {
                Name = lockerModel.Name,
                AllowedTime = lockerModel.AllowedTime,
                Link = lockerModel.Link
            };

            var entity = await _applicationContext.Lockers.AddAsync(locker);
            var createdLocker = entity.Entity;

            await _applicationContext.SaveChangesAsync();

            return Ok(createdLocker);

        }
    }
}