using System;
using System.Collections.Generic;
using System.Text;

namespace Wipro_training
{
    class Stack_Collections
    {
        static void Main(string[] args)
        {
            Stack<string> stack = new Stack<string>();

            stack.Push("Apple");
            stack.Push("banana");
            stack.Push("Cherry");

            Console.WriteLine("Stack contents:");
            foreach (string item in stack)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Is there any item you want to add??");
            string newItem = Console.ReadLine();
            stack.Push(newItem);

            List<string> list = new List<string>();
            foreach (string item in stack)
            {
                list.Add(item);
            }
            list.Remove("Cherry");
            Console.WriteLine("Updated list contents:");
            foreach (string item in list)
            {
                Console.WriteLine(item);
            }
            stack.Clear();
            Console.WriteLine("New and Updted stack contents:");
            foreach(string item in list)
            {
                stack.Push(item);
            }
            foreach (string item in stack)
            {
                Console.WriteLine(item);
            }
        }   
    }
}
