using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Services
{
    public class PaymentService
    {
        private readonly List<Payment> _payments = new();
        private int _nextId = 1;

        public List<Payment> GetAll()
        {
            return _payments;
        }

        public Payment? GetById(int id)
        {
            return _payments.FirstOrDefault(p => p.Id == id);
        }

        // Find the payment for a specific order
        public Payment? GetByOrderId(int orderId)
        {
            return _payments.FirstOrDefault(p => p.OrderId == orderId);
        }

        public Payment ProcessPayment(Payment payment)
        {
            payment.Id = _nextId;
            _nextId++;
            payment.ProcessedAt = DateTime.UtcNow;

            // Simulate a payment gateway response:
            // Cash always succeeds. Card/UPI succeeds 90% of the time (random simulation).
            bool paymentSucceeded;

            if (payment.Method == PaymentMethod.Cash)
            {
                paymentSucceeded = true;
            }
            else
            {
                // NextDouble() gives a random number between 0.0 and 1.0
                // If it's greater than 0.1, payment succeeds (90% chance)
                paymentSucceeded = new Random().NextDouble() > 0.1;
            }

            if (paymentSucceeded)
            {
                payment.Status = PaymentStatus.Completed;

                // Create a fake unique transaction ID (like a real gateway would return)
                // Guid.NewGuid() creates a unique ID, we take the first 8 characters
                string uniquePart = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                payment.TransactionId = "TXN-" + uniquePart;
            }
            else
            {
                payment.Status = PaymentStatus.Failed;
                payment.TransactionId = "";
            }

            _payments.Add(payment);
            return payment;
        }

        // Refund a payment — only allowed if it was successfully completed
        public bool RefundPayment(int id)
        {
            var payment = GetById(id);

            if (payment == null)
            {
                return false;
            }

            // Can only refund payments that are in "Completed" status
            if (payment.Status != PaymentStatus.Completed)
            {
                return false;
            }

            payment.Status = PaymentStatus.Refunded;
            return true;
        }
    }
}