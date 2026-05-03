using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityFeature
{
    internal class Program
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha1 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha1.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        static void Main(string[] args)
        {
            string HashValue = HashPassword("@admin124");
            Console.WriteLine(HashValue);
            Console.WriteLine("\n");

            string NewHashValue = Console.ReadLine();
            if (HashValue == NewHashValue)
            {
                Console.WriteLine("Password is correct");
            }
            else
            {
                Console.WriteLine("Password is incorrect");
            }
        }
    }
}
