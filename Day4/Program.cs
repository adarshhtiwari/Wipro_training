using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EcommerceSystem eCommerce_System = new EcommerceSystem();

            eCommerce_System.AddOrder(new Order { OrderId = 1, ProductName = "Laptop", Price = 999.99 });
            eCommerce_System.AddOrder(new Order { OrderId = 2, ProductName = "Mobile", Price = 459.99 });
            eCommerce_System.UpdateOrder(1, "Charger", 129.99);
            eCommerce_System.ProcessOrders();
            eCommerce_System.TrackOrderStatus();
        }
    }

    class Order
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; } = "";
        public double Price { get; set; }
    }

    class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }

    class EcommerceSystem
    {
        private List<Order> orders = new List<Order>();
        private Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
        private HashSet<string> productCategories = new HashSet<string>();
        private Queue<Order> orderProcessingQueue = new Queue<Order>();
        private Stack<string> orderStatusHistory = new Stack<string>();

        public void AddOrder(Order order)
        {
            orders.Add(order);
            orderProcessingQueue.Enqueue(order);
            productCategories.Add(order.ProductName);
            Console.WriteLine($"Order {order.OrderId} added.");
        }

        public void UpdateOrder(int orderId, string newProductName, double newPrice)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.ProductName = newProductName;
                order.Price = newPrice;
                Console.WriteLine($"Order {order.OrderId} updated.");
            }
            else
            {
                Console.WriteLine($"Order {orderId} Not Found.");
            }
        }

        public void ProcessOrders()
        {
            while (orderProcessingQueue.Count > 0)
            {
                var order = orderProcessingQueue.Dequeue();
                Console.WriteLine($"Processing Order {order.OrderId} for {order.ProductName} at ${order.Price}");
                orderStatusHistory.Push($"Order {order.OrderId} processed");
            }
        }

        public void TrackOrderStatus()
        {
            Console.WriteLine("Order Status History:");
            foreach (var status in orderStatusHistory)
            {
                Console.WriteLine(status);
            }
        }
    }
}