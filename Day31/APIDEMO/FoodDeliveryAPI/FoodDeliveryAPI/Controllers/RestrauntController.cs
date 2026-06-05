using Microsoft.AspNetCore.Mvc;
using FoodDeliveryAPI.Models;
using FoodDeliveryAPI.Services;

namespace FoodDeliveryAPI.Controllers
{
    // [ApiController] tells ASP.NET this class handles HTTP requests.
    // It also automatically validates incoming data and returns error messages if invalid.
    [ApiController]

    // [Route] sets the base URL for all endpoints in this controller.
    // "[controller]" is replaced with "restaurants" (the class name minus "Controller").
    // So all URLs here start with: /api/restaurants
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        // We store the service in a private field so all methods can use it
        private readonly RestaurantService _service;

        // This is a constructor. When ASP.NET creates this controller,
        // it automatically passes in a RestaurantService (from Program.cs registration).
        // This is called "Dependency Injection".
        public RestaurantsController(RestaurantService service)
        {
            _service = service;
        }

        // GET /api/restaurants
        // GET /api/restaurants?search=indian
        // [FromQuery] reads the value from the URL: ?search=indian
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? search)
        {
            if (string.IsNullOrEmpty(search))
            {
                // No search query — return everything
                return Ok(_service.GetAll());
            }
            else
            {
                // Filter by search term
                return Ok(_service.Search(search));
            }
        }

        // GET /api/restaurants/1
        // {id} in the route means the number from the URL is passed as the "id" parameter
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var restaurant = _service.GetById(id);

            if (restaurant == null)
            {
                // 404 Not Found — send a helpful message too
                return NotFound(new { message = $"Restaurant with ID {id} was not found." });
            }

            // 200 OK — send the restaurant data
            return Ok(restaurant);
        }

        // GET /api/restaurants/1/menu
        [HttpGet("{id}/menu")]
        public IActionResult GetMenu(int id)
        {
            var menu = _service.GetMenu(id);
            return Ok(menu);
        }

        // POST /api/restaurants
        // [FromBody] reads the JSON from the request body
        // Example body: { "name": "Burger Barn", "cuisine": "American", "address": "..." }
        [HttpPost]
        public IActionResult Add([FromBody] Restaurant restaurant)
        {
            var created = _service.Add(restaurant);

            // 201 Created — also tells the client the URL to access this new restaurant
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PATCH /api/restaurants/1/status?isOpen=false
        [HttpPatch("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromQuery] bool isOpen)
        {
            bool success = _service.UpdateStatus(id, isOpen);

            if (!success)
            {
                return NotFound(new { message = $"Restaurant with ID {id} was not found." });
            }

            string statusText = isOpen ? "open" : "closed";
            return Ok(new { message = $"Restaurant {id} is now {statusText}." });
        }

        // DELETE /api/restaurants/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = _service.Delete(id);

            if (!success)
            {
                return NotFound(new { message = $"Restaurant with ID {id} was not found." });
            }

            // 204 No Content — success, but nothing to return
            return NoContent();
        }
    }
}