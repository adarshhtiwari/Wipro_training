using Microsoft.AspNetCore.Mvc;
using FoodDeliveryAPI.Models;
using FoodDeliveryAPI.Services;

namespace FoodDeliveryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        // GET /api/orders
        // GET /api/orders?customer=Adarsh
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? customer)
        {
            if (string.IsNullOrEmpty(customer))
            {
                return Ok(_service.GetAll());
            }
            else
            {
                return Ok(_service.GetByCustomer(customer));
            }
        }

        // GET /api/orders/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _service.GetById(id);

            if (order == null)
            {
                return NotFound(new { message = $"Order with ID {id} was not found." });
            }

            return Ok(order);
        }

        // POST /api/orders
        // Example body:
        // {
        //   "restaurantId": 1,
        //   "customerName": "Adarsh",
        //   "deliveryAddress": "Wipro Office, Hyderabad",
        //   "items": [
        //     { "menuItemId": 1, "itemName": "Butter Chicken", "quantity": 2, "unitPrice": 280 }
        //   ]
        // }
        [HttpPost]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            var placed = _service.PlaceOrder(order);
            return CreatedAtAction(nameof(GetById), new { id = placed.Id }, placed);
        }

        // PATCH /api/orders/1/status?newStatus=Confirmed
        [HttpPatch("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromQuery] OrderStatus newStatus)
        {
            bool success = _service.UpdateStatus(id, newStatus);

            if (!success)
            {
                return NotFound(new { message = $"Order with ID {id} was not found." });
            }

            return Ok(new { message = $"Order {id} status updated to {newStatus}." });
        }

        // DELETE /api/orders/1/cancel
        [HttpDelete("{id}/cancel")]
        public IActionResult Cancel(int id)
        {
            bool success = _service.CancelOrder(id);

            if (!success)
            {
                return BadRequest(new { message = "Cannot cancel this order. It is already being prepared or has been delivered." });
            }

            return Ok(new { message = $"Order {id} has been cancelled." });
        }
    }
}