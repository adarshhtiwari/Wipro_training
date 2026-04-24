//With the help of indexing we can access the elements of an array or a collection using their index.
//Steps for implemting indexing in C#:
//Step 1: Define a class that will represent the collection you want to index.
//Step 2: Implement the indexer in the class. An indexer is defined using the 'this' keyword followed by square brackets [].
//Step 3: Inside the indexer, you can define the logic to get or set the value based on the index provided.
//Step 4: Create an instance of the class and use the indexer to access or modify the elements of the collection.

class MyCollection// This class represents a collection of integers and implements an indexer to access its elements.
{
    private int[] data = new int[10]; // This array will hold the integers in the collection.
                                      // Implementing the indexer
    public int this[int index] // This is the indexer definition. It allows us to access elements of the collection using an index.
    {
        get// The 'get' accessor is used to retrieve the value at the specified index.
        {
            if (index < 0 || index >= data.Length) // OR condition to check if the index is within the bounds of the array.
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
            return data[index];
        }

        set // The 'set' accessor is used to assign a value to the specified index.
        {
            if (index < 0 || index >= data.Length)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
            data[index] = value;

        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        MyCollection collection = new MyCollection();

        // ─────────────────────────────────────────
        // DEMO 1: Set values using the indexer
        // ─────────────────────────────────────────
        Console.WriteLine("=== Setting Values ===");
        for (int i = 0; i < 10; i++)
        {
            collection[i] = (i + 1) * 10;   // stores 10, 20, 30 ... 100
            Console.WriteLine($"collection[{i}] = {(i + 1) * 10}");
        }

        Console.WriteLine("\n=== Getting Values ===");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"collection[{i}] = {collection[i]}");
        }

        Console.WriteLine("Updating collection");
        collection[4] = 999;
        Console.WriteLine($"collection[4] is now = {collection[4]}");


        // Test negative index
        try
        {
            int val = collection[-1];   // should throw
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Caught (negative index): {ex.Message}");
        }

        // Test index beyond array length
        try
        {
            collection[10] = 500;       // should throw (valid indices: 0–9)
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Caught (index too large): {ex.Message}");
        }

        Console.WriteLine("\n=== Demo Complete ===");
    }
}