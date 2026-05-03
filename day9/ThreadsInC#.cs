using System;
using System.Threading;
using System.Threading.Tasks;

// Basic Thread 
class BasicThread
{
    static void PrintNumbers()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"[Thread] Count: {i}");
            Thread.Sleep(1000); // pause 1 second
        }
    }

    public static void Demo1()
    {
        Thread t1 = new Thread(PrintNumbers);
        t1.Start();
        // t1.Join(); // uncomment to make Main wait for t1
        Console.WriteLine("[Main] Main thread running...");
    }
}
// Without Join → Main and t1 run at the same time
// With Join   → Main waits for t1 to finish


//  Passing Data to Thread
class PassingDataToThread
{
    static void PrintMessage(object? msg)
    {
        Console.WriteLine($"[Thread] {msg}");
    }

    public static void Demo2()
    {
        // Way 1: Pass data via t.Start()
        Thread t1 = new Thread(PrintMessage);
        t1.Start("Hello via Start()");
        t1.Join();

        // Way 2: Lambda captures outer variable (closure)
        string text = "Hello via Closure";
        Thread t2 = new Thread(() => Console.WriteLine($"[Thread] {text}"));
        t2.Start();
        t2.Join();

        // Way 3: Lambda with explicit parameter
        Thread t3 = new Thread((object? msg) => Console.WriteLine($"[Thread] {msg}"));
        t3.Start("Hello via Lambda Param");
        t3.Join();
    }
}
// Way 1 → use when you already have a method defined
// Way 2 → use when you want to reuse a nearby variable
// Way 3 → use when you want an inline method with a parameter


// TASKS: Higher-level abstraction over threads, easier to use and manage
class UseOfTask
{
    static void MyMethod() => Console.WriteLine("[Task] Running MyMethod");

    public static void Demo3()
    {
        // Basic Task with lambda
        Task t1 = Task.Run(() => Console.WriteLine("[Task] Running inline"));
        t1.Wait();

        // Task using an existing method
        Task t2 = Task.Run(MyMethod);
        t2.Wait();

        // Task<T> returns a value
        Task<int> t3 = Task.Run(() =>
        {
            Thread.Sleep(2000); // simulate work
            return 42;
        });

        Console.WriteLine("[Main] Waiting for result...");
        Console.WriteLine($"[Main] Task returned: {t3.Result}"); // blocks until done
    }
}
// task.Wait()   → waits for task (like Thread.Join)
// Task<int>     → task that returns an int
// task.Result   → gets the value; blocks if task isn't done yet


// MAIN 
class Program
{
    static void Main()
    {
        BasicThread.Demo1();
        PassingDataToThread.Demo2();
        UseOfTask.Demo3();
    }
}