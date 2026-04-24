using System;
using System.Reflection;
using System.Collections.Generic;


namespace FeaturesCSharp
{
    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Student(string name, int age)
        {
            Name  = name;
            Age = age;
        }
    }
    class Box<T>
    {
        public T Value { get; set; }

        public Box(T value)
        {
            Value = value;
        }
        public void DisplayType()
        {
            Console.WriteLine($"Box contains:{Value}and type of T is: {typeof(T).Name}");
        }
    }

    class Program
    {
        static void Print<T>(T value)
        {
            Console.WriteLine($"Value: {value} and its type is {typeof(T).Name}");
        }

        static void UpdateValue(ref int value)
        {
        
                value += 10;
        }
        static void Calculate(int a, int b, out int sum, out int product)
        {
            sum = a + b;
            product = a * b;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\n");
            Type type = typeof(Student);
            Console.WriteLine("Class Name "+ type.Name);

            foreach(var prop in type.GetProperties())
            {
                Console.WriteLine($"Property: {prop.Name}, Type: {prop.PropertyType.Name}");
            }
            Console.WriteLine("\n");
            foreach(var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                Console.WriteLine($"Method: {method.Name}");
            }
            Console.WriteLine("\n");

            Box<int> intBox = new Box<int>(123);
            intBox.DisplayType();
            Box<string> strBox = new Box<string>("Hello Generics");
            intBox.DisplayType();

            Console.WriteLine("\n");

            Print<int>(123);
            Print<string>("Hello Generics again");
            Print<double>(3.14);
            Console.WriteLine("\n");

            int value = 5;
            Console.WriteLine("Before: " + value);
            UpdateValue(ref value);
            Console.WriteLine("After: " + value);
            Console.WriteLine("\n");

            int sum, product;
            Calculate(10, 25, out sum, out product);
            Console.WriteLine("Sum is: " + sum);
            Console.WriteLine("Product is:" + product);

        }
    }
}