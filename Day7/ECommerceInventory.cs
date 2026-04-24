using System;
using System.Collections.Generic;

class Product
{
    private string _name;
    private double _price;
    private int _quantity;

    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Product name cannot be empty.");
            _name = value;
        }
    }

    public double Price
    {
        get { return _price; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative.");
            _price = value;
        }
    }

    public int Quantity
    {
        get { return _quantity; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Quantity cannot be negative.");
            _quantity = value;
        }
    }

    // Constructor
    public Product(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"  Name     : {Name}\n" +
               $"  Price    : ₹{Price}\n" +
               $"  Quantity : {Quantity}";
    }
}

class Inventory
{
    // List to store products
    private List<Product> _products = new List<Product>();

    //Add Product to Inventory
    public void AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException("Product cannot be null.");

        _products.Add(product);
        Console.WriteLine($"Product '{product.Name}' added to inventory.");
    }

    // Access and modify product by index
    public Product this[int index]
    {
        get
        {
            if (index < 0 || index >= _products.Count)
                throw new IndexOutOfRangeException(
                    $"Index {index} is out of range. Valid range: 0 to {_products.Count - 1}.");
            return _products[index];
        }
        set
        {
            if (index < 0 || index >= _products.Count)
                throw new IndexOutOfRangeException(
                    $"Index {index} is out of range.");
            if (value == null)
                throw new ArgumentNullException("Product cannot be null.");

            //User Story 5: Update correct product
            _products[index] = value;
            Console.WriteLine($"Product at index {index} updated to '{value.Name}'.");
        }
    }

    // Indexer by string (product name)
    public Product this[string name]
    {
        get
        {
            Product found = _products.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (found == null)
                throw new KeyNotFoundException($"Product '{name}' not found.");

            return found;
        }
    }

    //Total Products
    public int Count
    {
        get { return _products.Count; }
    }

    //Display All Products 
    public void DisplayAll()
    {
        if (_products.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("Current Inventory:");
        for (int i = 0; i < _products.Count; i++)
        {
            Console.WriteLine($"[Index: {i}]");
            Console.WriteLine(_products[i]);
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Inventory inventory = new Inventory();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View Product by Index");
            Console.WriteLine("3. View Product by Name");
            Console.WriteLine("4. Update Product by Index");
            Console.WriteLine("5. Update Price by Index");
            Console.WriteLine("6. Update Quantity by Index");
            Console.WriteLine("7. Display All Products");
            Console.WriteLine("8. Exit");
            Console.Write("Choose option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            try
            {
                switch (choice)
                {
                    //Add Product
                    case 1:
                        Console.Write("Enter product name: ");
                        string name = Console.ReadLine()?.Trim();

                        Console.Write("Enter product price:$");
                        if (!double.TryParse(Console.ReadLine(), out double price))
                        {
                            Console.WriteLine("Invalid price.");
                            break;
                        }

                        Console.Write("Enter product quantity : ");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.WriteLine("Invalid quantity.");
                            break;
                        }

                        inventory.AddProduct(new Product(name, price, quantity));
                        break;

                    case 2:
                        Console.Write("Enter index: ");
                        if (!int.TryParse(Console.ReadLine(), out int viewIndex))
                        {
                            Console.WriteLine("Invalid index.");
                            break;
                        }

                        Product viewedProduct = inventory[viewIndex];
                        Console.WriteLine($"Product at index {viewIndex}:");
                        Console.WriteLine(viewedProduct);
                        break;

                    case 3:
                        Console.Write("Enter product name: ");
                        string searchName = Console.ReadLine()?.Trim();
                        Product foundProduct = inventory[searchName];
                        Console.WriteLine($"\n📦 Product '{searchName}':");
                        Console.WriteLine(foundProduct);
                        break;

                    case 4:
                        Console.Write("Enter index to update  : ");
                        if (!int.TryParse(Console.ReadLine(), out int updateIndex))
                        {
                            Console.WriteLine("Invalid index.");
                            break;
                        }

                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine()?.Trim();

                        Console.Write("Enter new price: ₹");
                        if (!double.TryParse(Console.ReadLine(), out double newPrice))
                        {
                            Console.WriteLine("Invalid price.");
                            break;
                        }

                        Console.Write("Enter new quantity     : ");
                        if (!int.TryParse(Console.ReadLine(), out int newQty))
                        {
                            Console.WriteLine("Invalid quantity.");
                            break;
                        }

                        inventory[updateIndex] = new Product(newName, newPrice, newQty);
                        break;

                    case 5:
                        Console.Write("Enter index: ");
                        if (!int.TryParse(Console.ReadLine(), out int priceIndex))
                        {
                            Console.WriteLine("Invalid index.");
                            break;
                        }

                        Console.Write("Enter new price: ₹");
                        if (!double.TryParse(Console.ReadLine(), out double updatedPrice))
                        {
                            Console.WriteLine("Invalid price.");
                            break;
                        }

                        // Access via indexer then update property
                        inventory[priceIndex].Price = updatedPrice;
                        Console.WriteLine($"Price updated to ₹{updatedPrice}.");
                        break;

                    // ── Update Quantity Only ──────────
                    case 6:
                        Console.Write("Enter index: ");
                        if (!int.TryParse(Console.ReadLine(), out int qtyIndex))
                        {
                            Console.WriteLine("Invalid index.");
                            break;
                        }

                        Console.Write("Enter new quantity : ");
                        if (!int.TryParse(Console.ReadLine(), out int updatedQty))
                        {
                            Console.WriteLine("Invalid quantity.");
                            break;
                        }

                        inventory[qtyIndex].Quantity = updatedQty;
                        Console.WriteLine($"Quantity updated to {updatedQty}.");
                        break;

                    case 7:
                        inventory.DisplayAll();// for dispaying all the products in the inventory
                        break;

                    case 8:
                        exit = true;
                        Console.WriteLine("Bye!, Have a nice day");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Index Error : {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Argument Error : {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Not Found : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }
    }
}