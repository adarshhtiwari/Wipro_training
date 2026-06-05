using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Services
{
    public class DeliveryService
    {
        // Pre-loaded delivery agents (like seed data)
        private readonly List<DeliveryAgent> _agents = new()
        {
            new() { Id = 1, Name = "Ravi Kumar",  Phone = "9876543210", VehicleType = "Bike",    Status = AgentStatus.Available },
            new() { Id = 2, Name = "Priya Nair",  Phone = "9876543211", VehicleType = "Scooter", Status = AgentStatus.Available },
            new() { Id = 3, Name = "Arun Sharma", Phone = "9876543212", VehicleType = "Bike",    Status = AgentStatus.Offline  }
        };

        // Stores live tracking info for active deliveries
        private readonly List<DeliveryTracking> _trackingRecords = new();

        public List<DeliveryAgent> GetAllAgents()
        {
            return _agents;
        }

        public DeliveryAgent? GetAgentById(int id)
        {
            return _agents.FirstOrDefault(a => a.Id == id);
        }

        // Automatically finds a free agent and assigns them to the order
        public DeliveryAgent? AssignAgent(int orderId)
        {
            // Find the first agent who is available right now
            var agent = _agents.FirstOrDefault(a => a.Status == AgentStatus.Available);

            // If no agent is free, return null — the controller will handle this
            if (agent == null)
            {
                return null;
            }

            // Mark the agent as busy
            agent.Status = AgentStatus.OnDelivery;
            agent.CurrentOrderId = orderId;

            // Create a tracking entry so customers can see live updates
            var trackingEntry = new DeliveryTracking
            {
                AgentId = agent.Id,
                OrderId = orderId,
                CurrentLocation = "Picked up from restaurant",
                EstimatedArrival = "30 minutes",
                UpdatedAt = DateTime.UtcNow
            };
            _trackingRecords.Add(trackingEntry);

            return agent;
        }

        // Get live tracking info for an order
        public DeliveryTracking? GetTracking(int orderId)
        {
            return _trackingRecords.FirstOrDefault(t => t.OrderId == orderId);
        }

        // Update the agent's current location (called by the agent's app)
        public bool UpdateLocation(int orderId, string location, string eta)
        {
            var tracking = GetTracking(orderId);

            if (tracking == null)
            {
                return false;
            }

            tracking.CurrentLocation = location;
            tracking.EstimatedArrival = eta;
            tracking.UpdatedAt = DateTime.UtcNow;
            return true;
        }

        // Mark an order as delivered and free up the agent
        public bool CompleteDelivery(int orderId)
        {
            var tracking = GetTracking(orderId);

            if (tracking == null)
            {
                return false;
            }

            // Free up the agent so they can take another order
            var agent = GetAgentById(tracking.AgentId);
            if (agent != null)
            {
                agent.Status = AgentStatus.Available;
                agent.CurrentOrderId = null;  // null means they have no current order
            }

            tracking.CurrentLocation = "Delivered";
            tracking.EstimatedArrival = "Arrived";
            return true;
        }
    }
}