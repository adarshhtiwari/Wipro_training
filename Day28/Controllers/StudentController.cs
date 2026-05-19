using Microsoft.AspNetCore.Mvc;
using StudentAjaxApp.Models;

namespace StudentAjaxApp.Controllers
{
    public class StudentController : Controller
    {
        // Dummy student data
        List<Student> students = new List<Student>()
        {
            new Student { StudentId = 101, Name = "Rahul", Course = "CSE", Age = 21 },
            new Student { StudentId = 102, Name = "Aman", Course = "ECE", Age = 22 },
            new Student { StudentId = 103, Name = "Priya", Course = "IT", Age = 20 }
        };

        public IActionResult Index()
        {
            return View();
        }

        // AJAX METHOD
        [HttpGet]
        public JsonResult SearchStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Student Not Found"
                });
            }

            return Json(new
            {
                success = true,
                data = student
            });
        }
    }
}