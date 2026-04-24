using System;
using System.IO;

namespace FileHandling

{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "example.txt";
            // Write to a file
            System.IO.File.WriteAllText(filePath, "Hello");
            // Read from a file
            string content = System.IO.File.ReadAllText(filePath);
            Console.WriteLine(content);
            // Append to a file
            System.IO.File.AppendAllText(filePath, "\nWelcome here");
            // Read the updated content
            string updatedContent = System.IO.File.ReadAllText(filePath);
            Console.WriteLine(updatedContent);
            // Delete the file
            //System.IO.File.Delete(filePath);




            string copyPath = "example_copy.txt";

            if (File.Exists(copyPath))
            {
                // overwrite = true means replace if already exists
                File.Copy(filePath, copyPath, overwrite: true);
                Console.WriteLine("File was overwritten successfully.");
            }
            else
            {
                File.Copy(filePath, copyPath);
                Console.WriteLine("File copied successfully.");
            }

            // Read the copied file to verify
            string copiedContent = File.ReadAllText(copyPath);
            Console.WriteLine("\nCopied File Content:");
            Console.WriteLine(copiedContent);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("example.txt deleted successfully.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
    }
}
