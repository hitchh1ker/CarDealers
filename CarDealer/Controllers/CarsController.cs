using CarDealers.Repository.Car.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Controllers
{
    public class CarsController : ControllerBase
    {
        private readonly CarDataContext _carDataContext;

        public CarsController(CarDataContext carDataContext)
        {
            _carDataContext = carDataContext;
        }

        [HttpGet("/cars")]
        public async Task<IActionResult> GetCarsAsync(int page = 1)
        {
            int pageSize = 1;

            var cars = await _carDataContext.GetPagedAsync(page, pageSize);
            int totalRecords = await _carDataContext.GetTotalCountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            if (page < 1 || page > totalPages)
            {
                return BadRequest();
            }

            return Ok(cars);
        }
    }
}