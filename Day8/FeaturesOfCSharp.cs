using System;
using System.Collections.Generic;

class DemoCSharpFeatures
{
    // Delegate declaration (Image 2)
    delegate void displayMessage(string message);

    static void Main(string[] args)
    {
        displayMessage msg = delegate (string text)
        { Console.WriteLine(text); };

        // calling an anonymous method
        msg("This is a text message coming from an Anonymous method call ...!!");

        // above task can be done in a line using lambda expression in C#
        Console.WriteLine("If we are using lambda expression then we can do this in a line ..!!");
        displayMessage MyMsg = (text) => Console.WriteLine(text);
        MyMsg("here is a text displayed using Lambda Expression in C# ");
        // ── Func ──────────────────────────────────────────────────────────
        Func<int, int, int> add = (a, b) => a + b;
        Console.WriteLine("Sum of two number is " + add(4, 9));

        // ── Action ────────────────────────────────────────────────────────
        Console.WriteLine("Action<T> delegate in action ");
        Action<string> print = (newmsg) => Console.WriteLine(newmsg);
        print("Here is a demo of Action<T> delegate that doesn't return a value");

        // ── Predicate ─────────────────────────────────────────────────────
        // defining a predicate delegate isEven()
        Predicate<int> isEven = (num) => num % 2 == 0;
        Console.WriteLine("is 10 even " + isEven(10));

        // using predicate on a list to check for even no
        List<int> list = new List<int> { 1, 5, 6, 9, 10 };
        var evenNumber = list.FindAll(isEven);
        Console.WriteLine("This is a filtered list of even no " + string.Join(",", evenNumber));
    }
}