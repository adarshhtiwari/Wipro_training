//Problem statement based on use of delgates in C#:
//1. Create a delegate type called "MathOperation" that takes two integers as parameters and
//returns an integer as output.
//2. Implement three methods that match the signature of the "MathOperation" delegate:
//   a. Add: This method should return the sum of the two integers.
//   b. Subtract: This method should return the difference of the two integers.
//   c. Multiply: This method should return the product of the two integers.
//3. Create an instance of the "MathOperation" delegate and assign it the "Add" method.
//4. Invoke the delegate with two integers and display the result.
//5. Change the delegate assignment to the "Subtract" method and invoke it again with the same integers, displaying the result.//Creating an instance of the "MathOperation" delegate and assigning it the "Add" method.
using System;

using Delegates_demoCsharp;
class Program
{
    static void Main()
    {
        int result = MathOperations.Add(5, 3);
        Console.WriteLine(result);

        int result2 = MathOperations.Subtract(5, 3); 
        Console.WriteLine(result2);
    }
}
//define a delegate type called "MathOperation" that takes two integers as parameters and returns an integer as output.
//outside the main method of the class and before the main method of the class

