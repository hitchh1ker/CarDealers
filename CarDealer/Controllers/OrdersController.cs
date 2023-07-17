using CarDealer.Models.Orders;
using CarDealers.Repository.Car.Models;
using CarDealers.Repository.Order.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarDealer.Controllers
{
    public class OrdersController : ControllerBase
    {
        private readonly OrderDataContext _orderDataContext;
        public OrdersController(OrderDataContext orderDataContext)
        {
            _orderDataContext = orderDataContext;
        }

        [HttpGet("/orders")]
        public async Task<IActionResult> GetOrdersAsync(int page = 1)
        {
            int pageSize = 1;

            var orders = await _orderDataContext.GetPagedAsync(page, pageSize);
            int totalRecords = await _orderDataContext.GetTotalCountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            
            if (page < 1 || page > totalPages)
            {
                return BadRequest();
            }

            return Ok(orders);
        }

        [HttpPost("/order")]
        public async Task<IActionResult> PostOrderAsync([FromBody] CreateOrderRequest request)
        {
            try
            {
                Order order = new Order
                {
                    CarId = request.CarId,
                    EmployeeId = request.EmployeeId,
                    ClientId = request.ClientId,
                    Cost = request.Cost
                };

                await _orderDataContext.InsertAsync(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpDelete("/order/{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            try
            {
                await _orderDataContext.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpPut("/order/{id}/cost")]
        public async Task<IActionResult> UpdateCostOrderAsync(int id,[FromBody] UpdateCostOrderRequest request)
        {
            try
            {
                Order order = new Order
                {
                    Id = id,
                    Cost = request.Cost
                };

                await _orderDataContext.UpdateCostByIdAsync(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }

        [HttpPut("/order/{id}/status")]
        public async Task<IActionResult> UpdateStatusOrderAsync(int id, [FromBody] UpdateStatusOrderRequest request)
        {
            try
            {
                Order order = new Order
                {
                    Id = id,
                    Cost = request.StatusId
                };

                await _orderDataContext.UpdateStatusByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }
    }
}
