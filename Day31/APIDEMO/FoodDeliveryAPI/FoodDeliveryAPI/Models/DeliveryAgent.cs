namespace FoodDeliveryAPI.Models
{
    // Tracks what a delivery agent is currently doing
    public enum AgentStatus
    {
        Available,   // Free to take a new delivery
        OnDelivery,  // Currently delivering an order
        Offline      // Not working right now
    }

    // Represents a delivery agent (person who delivers food)
    public class DeliveryAgent
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;  // e.g. "Bike", "Scooter"
        public AgentStatus Status { get; set; } = AgentStatus.Available;

        // The "?" after int means this can be null (no order assigned yet)
        public int? CurrentOrderId { get; set; }
    }

    // Tracks where a delivery agent is right now, for a specific order
    public class DeliveryTracking
    {
        public int AgentId { get; set; }
        public int OrderId { get; set; }
        public string CurrentLocation { get; set; } = string.Empty;  // e.g. "Near Cyber Towers"
        public string EstimatedArrival { get; set; } = string.Empty; // e.g. "10 minutes"
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}