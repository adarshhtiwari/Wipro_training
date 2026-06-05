using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Services
{
    public class OrderService
    {
        // Starts empty — orders are added when customers place them
        private readonly List<Order> _orders = new();
        private int _nextId = 1;

        public List<Order> GetAll()
        {
            return _orders;
        }

        public Order? GetById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        // Returns all orders placed by a specific customer
        public List<Order> GetByCustomer(string customerName)
        {
            return _orders.Where(o =>
                o.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        public Order PlaceOrder(Order order)
        {
            order.Id = _nextId;
            _nextId++;

            // Calculate the total price from all items
            // Instead of trusting whatever the client sends, we recalculate it ourselves
            // This prevents customers from sending a fake low total
            decimal total = 0;
            foreach (var item in order.Items)
            {
                total += item.Quantity * item.UnitPrice;
            }
            order.TotalAmount = total;

            order.Status = OrderStatus.Placed;
            order.PlacedAt = DateTime.UtcNow;

            _orders.Add(order);
            return order;
        }

        // Updates the status of an order (e.g. from Placed to Confirmed)
        public bool UpdateStatus(int id, OrderStatus newStatus)
        {
            var order = GetById(id);
            if (order == null)
            {
                return false;
            }

            order.Status = newStatus;
            return true;
        }

        // Cancels an order — but only if it hasn't started being prepared yet
        public bool CancelOrder(int id)
        {
            var order = GetById(id);

            if (order == null)
            {
                return false;
            }

            // Enums can be compared with >= because they have underlying numbers:
            // Placed=0, Confirmed=1, Preparing=2, OutForDelivery=3, Delivered=4, Cancelled=5
            // If status is Preparing or beyond, it's too late to cancel
            if (order.Status >= OrderStatus.Preparing)
            {
                return false;
            }

            order.Status = OrderStatus.Cancelled;
            return true;
        }
    }
}