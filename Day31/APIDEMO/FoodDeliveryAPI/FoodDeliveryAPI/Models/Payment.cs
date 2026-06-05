namespace FoodDeliveryAPI.Models
{
    // The possible states a payment can be in
    public enum PaymentStatus
    {
        Pending,    // Payment was created but not yet processed
        Completed,  // Payment went through successfully
        Failed,     // Payment was declined
        Refunded    // Money was sent back to the customer
    }

    // The ways a customer can pay
    public enum PaymentMethod
    {
        CreditCard,
        DebitCard,
        UPI,
        Cash
    }

    public class Payment
    {
        public int Id { get; set; }

        // Links this payment to an order
        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        // Which method the customer chose (UPI, Cash, etc.)
        public PaymentMethod Method { get; set; }

        // Starts as Pending until the gateway processes it
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;

        // A unique ID returned by the payment gateway (like a receipt number)
        public string TransactionId { get; set; } = string.Empty;
    }
}