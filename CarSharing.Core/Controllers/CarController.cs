using CarSharing.Core.Entities.Car;
using CarSharing.Core.Repository;
using CarSharing.Entities.Car;
using Mapster;
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
            if (result.Count > 0)
            {
                return Ok(result);
            }
            return NoContent();
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<Car>> GetBy(Guid guid)
        {
            var result = await _manager.Cars.GetByExpression(car => car.Id == guid);
            if (result.Count != 0)
            {
                return Ok(result);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CarViewModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newCar = carModel.Adapt<Car>();
            try
            {
                await _manager.Cars.Create(newCar);
                await _manager.Save;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created(nameof(GetBy), new { guid = newCar.Id });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid guid)
        {
            var car = await _manager.Cars.GetByExpression(c => c.Id == guid);
            if (car != null)
            {
                try
                {
                    await _manager.Cars.Delete(car.First());
                    await _manager.Save;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Car>> Update([FromQuery] Guid guid, [FromBody] DateTime newRentDate)
        {
            var currentRentCar = await _manager.Cars
                .GetByExpression(car => car.Id == guid);

            if (currentRentCar == null) return NotFound();
            try
            {
                await _manager.Cars.Update(currentRentCar[0]);
                await _manager.Save;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(GetBy), new { guid = guid });
        }
    }
}
