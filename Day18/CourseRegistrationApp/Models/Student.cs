using System.ComponentModel.DataAnnotations;

namespace CourseRegistrationApp.Models
 
{//This is my model class with fields where validation will be implemted with
 //data annotations 
    public class Student
    {
        [Required(ErrorMessage ="Name is required..!!")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Email Is required..!!")]
        public string Email { get; set; }

        [Range (18,60, ErrorMessage ="Age must be between 18 to 60")]
        public int Age { get; set; }

    }
}
