namespace FoodDeliveryAPI.Models
{
    // An "enum" is a list of allowed values.
    // Instead of storing "1" or "2", we store readable names like "Placed" or "Delivered".
    // This makes the code much easier to understand.
    public enum OrderStatus
    {
        Placed,          // Step 1: Customer placed the order
        Confirmed,       // Step 2: Restaurant accepted it
        Preparing,       // Step 3: Kitchen is cooking
        OutForDelivery,  // Step 4: Agent picked it up
        Delivered,       // Step 5: Customer received it
        Cancelled        // Order was cancelled at some point
    }

    // This class describes a customer's order
    public class Order
    {
        public int Id { get; set; }

        // Which restaurant the order is from
        public int RestaurantId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string DeliveryAddress { get; set; } = string.Empty;

        // One order can have multiple items (e.g. 2 dishes + 1 drink)
        public List<OrderItem> Items { get; set; } = new();

        // This is calculated automatically in the service — don't send it from the client
        public decimal TotalAmount { get; set; }

        // When a new Order is created, status starts at "Placed"
        public OrderStatus Status { get; set; } = OrderStatus.Placed;

        // DateTime.UtcNow gives the current date and time (in UTC timezone)
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
    }

    // Describes one line item inside an order (e.g. "2x Butter Chicken at ₹280 each")
    public class OrderItem
    {
        public int MenuItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}