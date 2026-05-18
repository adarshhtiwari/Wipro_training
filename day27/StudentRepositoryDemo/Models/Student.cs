using System.ComponentModel.DataAnnotations;

namespace StudentRepositoryDemo.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Course { get; set; }

        public int Age { get; set; }
    }
}