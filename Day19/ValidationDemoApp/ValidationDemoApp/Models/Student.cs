

using System.ComponentModel.DataAnnotations;
namespace ValidationDemoApp.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Name is required.")]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Range (18, 60)]
        public int Age { get; set; }
    }
}
