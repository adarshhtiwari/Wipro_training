
using System;

class Program
{
    // Delegate
    public delegate void MyDelegate();

    // Methods
    static void Method1()
    {
        Console.WriteLine("Method 1 executed");
    }

    static void Method2()
    {
        Console.WriteLine("Method 2 executed");
    }

    static void Main()
    {
        MyDelegate del = Method1;   // assign first method
        del += Method2;  // add second method

        del();  // calls both methods
    }
}