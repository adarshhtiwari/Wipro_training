// A "namespace" is like a folder for your code.
// It helps organize classes and avoid naming conflicts.
namespace FoodDeliveryAPI.Models
{
    // A "class" is a blueprint. This one describes what a Restaurant looks like.
    // Think of it like a form — every restaurant fills in these fields.
    public class Restaurant
    {
        // "public" means other files can access this.
        // "int" means it holds a whole number.
        // "{ get; set; }" means you can both read and write this value.
        public int Id { get; set; }

        // "string" holds text. We set it to empty by default so it's never null.
        public string Name { get; set; } = string.Empty;

        public string Cuisine { get; set; } = string.Empty;  // e.g. "Indian", "Italian"

        public string Address { get; set; } = string.Empty;

        // "bool" holds true or false. New restaurants are open by default.
        public bool IsOpen { get; set; } = true;

        // A restaurant has many menu items, so we use a List.
        // "new()" creates an empty list so we don't get a null error.
        public List<MenuItem> Menu { get; set; } = new();
    }

    // This class describes a single item on the menu (e.g. "Butter Chicken")
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // "decimal" is used for money — more precise than double for currency
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}