using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Services
{
    // The service holds all the business logic for restaurants.
    // The controller will call methods from here — keeping things organized.
    public class RestaurantService
    {
        // This is our fake database — just a list stored in memory.
        // In a real app you'd use a proper database like SQL Server.
        // "readonly" means this list can't be replaced, but items can be added/removed.
        private readonly List<Restaurant> _restaurants = new()
        {
            new Restaurant
            {
                Id = 1,
                Name = "Spice Garden",
                Cuisine = "Indian",
                Address = "12 MG Road, Hyderabad",
                IsOpen = true,
                Menu = new List<MenuItem>
                {
                    new() { Id = 1, Name = "Butter Chicken", Price = 280, IsAvailable = true },
                    new() { Id = 2, Name = "Garlic Naan",    Price = 50,  IsAvailable = true },
                    new() { Id = 3, Name = "Mango Lassi",    Price = 80,  IsAvailable = true }
                }
            },
            new Restaurant
            {
                Id = 2,
                Name = "Pizza Primo",
                Cuisine = "Italian",
                Address = "5 Banjara Hills, Hyderabad",
                IsOpen = true,
                Menu = new List<MenuItem>
                {
                    new() { Id = 4, Name = "Margherita Pizza", Price = 320, IsAvailable = true  },
                    new() { Id = 5, Name = "Pasta Arrabiata",  Price = 260, IsAvailable = false },
                    new() { Id = 6, Name = "Tiramisu",         Price = 150, IsAvailable = true  }
                }
            }
        };

        // Tracks the next ID to assign when a new restaurant is added
        private int _nextId = 3;

        // Returns all restaurants in the list
        public List<Restaurant> GetAll()
        {
            return _restaurants;
        }

        // Searches for one restaurant by its ID.
        // Returns null if not found — the "?" after Restaurant means it can return null.
        public Restaurant? GetById(int id)
        {
            // FirstOrDefault loops through the list and returns the first match,
            // or null if nothing matches.
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        // Searches restaurants by name or cuisine type (case-insensitive)
        public List<Restaurant> Search(string query)
        {
            return _restaurants.Where(r =>
                r.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                r.Cuisine.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        // Adds a new restaurant to the list
        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = _nextId;  // Assign the next available ID
            _nextId++;                // Increment so the next one gets a different ID
            _restaurants.Add(restaurant);
            return restaurant;        // Return it so the controller can send it back in the response
        }

        // Opens or closes a restaurant
        public bool UpdateStatus(int id, bool isOpen)
        {
            var restaurant = GetById(id);

            // If the restaurant wasn't found, return false to signal failure
            if (restaurant == null)
            {
                return false;
            }

            restaurant.IsOpen = isOpen;
            return true;
        }

        // Returns just the menu for a specific restaurant
        public List<MenuItem> GetMenu(int restaurantId)
        {
            var restaurant = GetById(restaurantId);

            // If restaurant doesn't exist, return an empty list instead of crashing
            if (restaurant == null)
            {
                return new List<MenuItem>();
            }

            return restaurant.Menu;
        }

        // Removes a restaurant from the list
        public bool Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant == null)
            {
                return false;
            }

            _restaurants.Remove(restaurant);
            return true;
        }
    }
}