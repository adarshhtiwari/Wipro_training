using Microsoft.AspNetCore.Mvc;
using FoodDeliveryAPI.Models;
using FoodDeliveryAPI.Services;

namespace FoodDeliveryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _service;

        public PaymentsController(PaymentService service)
        {
            _service = service;
        }

        // GET /api/payments
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        // GET /api/payments/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var payment = _service.GetById(id);

            if (payment == null)
            {
                return NotFound(new { message = $"Payment with ID {id} was not found." });
            }

            return Ok(payment);
        }

        // GET /api/payments/order/1
        [HttpGet("order/{orderId}")]
        public IActionResult GetByOrderId(int orderId)
        {
            var payment = _service.GetByOrderId(orderId);

            if (payment == null)
            {
                return NotFound(new { message = $"No payment found for order {orderId}." });
            }

            return Ok(payment);
        }

        // POST /api/payments
        // Example body: { "orderId": 1, "amount": 330.00, "method": 2 }
        // method: 0 = CreditCard, 1 = DebitCard, 2 = UPI, 3 = Cash
        [HttpPost]
        public IActionResult ProcessPayment([FromBody] Payment payment)
        {
            var result = _service.ProcessPayment(payment);

            if (result.Status == PaymentStatus.Failed)
            {
                // 400 Bad Request — payment didn't go through
                return BadRequest(new { message = "Payment failed. Please try again.", payment = result });
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // POST /api/payments/1/refund
        [HttpPost("{id}/refund")]
        public IActionResult Refund(int id)
        {
            bool success = _service.RefundPayment(id);

            if (!success)
            {
                return BadRequest(new { message = "Refund failed. Payment must exist and be in Completed status." });
            }

            return Ok(new { message = $"Payment {id} has been refunded successfully." });
        }
    }
}