using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            return await Task.Run(() => "Hello World");
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<string> GetAll()
        {
            return await Task.Run(() => "return all");
        }
    }
}
