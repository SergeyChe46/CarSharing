using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSharing.Core.Repository;
using CarSharing.Entities.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly RepositoryManager _manager;
        public CarController(RepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetAll()
        {
            List<Car> result = await _manager.Cars.GetAll();
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        // [HttpGet("all")]
        // [Authorize]
        // public async Task<string> GetAll()
        // {
        //     return await Task.Run(() => "Hello");
        // }
    }
}
