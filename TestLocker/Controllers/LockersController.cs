using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TestLocker.Data;
using TestLocker.Models;

namespace TestLocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LockersController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public LockersController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Locker>> Index()
        {
            return _applicationContext.Lockers.ToList();
        }

        [HttpGet("/details")]
        public ActionResult<Locker> Details(Guid id)
        {
            return _applicationContext.Lockers.FirstOrDefault(locker => locker.Id == id);
        }

        [HttpPost]
        public ActionResult<Locker> Create([FromBody]Locker locker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _applicationContext.Lockers.Add(locker);

            return Created(nameof(Details), locker.Id);

        }
    }
}