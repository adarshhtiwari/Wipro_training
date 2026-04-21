using System;


namespace day5
{
    public class UseOfTuples
    
    {
        static(double area, double perimeter) CalculateAreaAndPerimeter(double length, double breadth)
        {
            double area = length * breadth;
            double perimeter = 2 * (length + breadth);
            return (area, perimeter);
        }   
        
        public static void Main()
        {
            Console.WriteLine("Tuple is " + CalculateAreaAndPerimeter(5, 5));
            var result = CalculateAreaAndPerimeter(5, 5);
            Console.WriteLine("Area: " + result.area + ", Perimerter: " + result.perimeter);

        }
    }
}