using Microsoft.AspNetCore.Mvc;
using FoodDeliveryAPI.Models;
using FoodDeliveryAPI.Services;

namespace FoodDeliveryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly DeliveryService _service;

        public DeliveryController(DeliveryService service)
        {
            _service = service;
        }

        // GET /api/delivery/agents
        [HttpGet("agents")]
        public IActionResult GetAllAgents()
        {
            return Ok(_service.GetAllAgents());
        }

        // GET /api/delivery/agents/1
        [HttpGet("agents/{id}")]
        public IActionResult GetAgent(int id)
        {
            var agent = _service.GetAgentById(id);

            if (agent == null)
            {
                return NotFound(new { message = $"Agent with ID {id} was not found." });
            }

            return Ok(agent);
        }

        // POST /api/delivery/assign/1
        // Finds a free agent and assigns them to the given order
        [HttpPost("assign/{orderId}")]
        public IActionResult AssignAgent(int orderId)
        {
            var agent = _service.AssignAgent(orderId);

            if (agent == null)
            {
                // 503 Service Unavailable — no agents free right now
                return StatusCode(503, new { message = "No delivery agents are available right now. Please try again shortly." });
            }

            return Ok(new { message = $"Agent '{agent.Name}' has been assigned to order {orderId}.", agent });
        }

        // GET /api/delivery/track/1
        [HttpGet("track/{orderId}")]
        public IActionResult Track(int orderId)
        {
            var tracking = _service.GetTracking(orderId);

            if (tracking == null)
            {
                return NotFound(new { message = "No tracking info available yet. Make sure an agent has been assigned first." });
            }

            return Ok(tracking);
        }

        // PATCH /api/delivery/track/1/location
        // Body: { "location": "Near Cyber Towers", "eta": "10 minutes" }
        [HttpPatch("track/{orderId}/location")]
        public IActionResult UpdateLocation(int orderId, [FromBody] LocationUpdate update)
        {
            bool success = _service.UpdateLocation(orderId, update.Location, update.Eta);

            if (!success)
            {
                return NotFound(new { message = $"No tracking record found for order {orderId}." });
            }

            return Ok(new { message = "Location updated successfully." });
        }

        // POST /api/delivery/complete/1
        [HttpPost("complete/{orderId}")]
        public IActionResult Complete(int orderId)
        {
            bool success = _service.CompleteDelivery(orderId);

            if (!success)
            {
                return NotFound(new { message = $"No active delivery found for order {orderId}." });
            }

            return Ok(new { message = $"Order {orderId} has been delivered! The agent is now available for new orders." });
        }
    }

    // A simple helper class to hold the request body for the location update endpoint
    public class LocationUpdate
    {
        public string Location { get; set; } = string.Empty;  // e.g. "Near Cyber Towers"
        public string Eta { get; set; } = string.Empty;       // e.g. "10 minutes"
    }
}