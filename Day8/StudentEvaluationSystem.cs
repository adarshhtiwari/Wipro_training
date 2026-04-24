using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Student
    {
        public string Name { get; set; }
        public int Marks { get; set; }

        public Student(string name, int marks)  
        {
            Name = name;
            Marks = marks;
        }
    }

    class Program
    {
        delegate void StudentDelegate(Student student);

        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student("Alice", 85),
                new Student("Bob", 92),
                new Student("Charlie", 78),
                new Student("David", 90),
                new Student("Eve", 88),
                new Student("Frank", 45)
            };

            Func<List<Student>, int> TotalMarks = (studentList) =>
            {
                int total = studentList.Sum(s => s.Marks);
                return total;
            };

            int totalMarks = TotalMarks(students);
            Console.WriteLine("Total marks of students: " + totalMarks);
            Console.WriteLine("\n");

            Action<Student> PrintStudentInfo = (student) =>
            {
                Console.WriteLine($"Name: {student.Name}, Marks: {student.Marks}");
            };
            students.ForEach(PrintStudentInfo);
            Console.WriteLine("\n");
            Predicate<Student> isEligible = s => s.Marks >= 50;
            foreach (var s in students)
            {
                if (isEligible(s))
                {
                    Console.WriteLine($"Name: {s.Name} is eligible.");
                }
                else
                {
                    Console.WriteLine($"Name: {s.Name} is not eligible.");
                }
            }
            Console.WriteLine("\n");
            Predicate<Student> isTopScorer = s => s.Marks >= 75;

            List<Student> topScorers = students.Where(s => isTopScorer(s)).ToList();
            foreach (var s in topScorers)
            {
                Console.WriteLine($"Name: {s.Name} is a top scorer.");
            }
            Console.WriteLine("\n");
            StudentDelegate printStudent = delegate (Student s)
            {
                Console.WriteLine($"Name: {s.Name}, Marks: {s.Marks}");
            };
            students.ForEach(s => printStudent(s));

            Console.WriteLine("\n");
            StudentDelegate PrintLambda = s => Console.WriteLine($"Name: {s.Name}, Marks: {s.Marks}");
            students.ForEach(s => PrintLambda(s));
            Console.ReadLine();
        }
    }
}