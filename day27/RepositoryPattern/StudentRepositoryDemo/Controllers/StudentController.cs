using Microsoft.AspNetCore.Mvc;
using StudentRepositoryDemo.Models;
using StudentRepositoryDemo.Repository;

namespace StudentRepositoryDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _repository;

        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }

        // DISPLAY ALL STUDENTS
        public IActionResult Index()
        {
            var students = _repository.GetAllStudents();

            return View(students);
        }

        // OPEN CREATE PAGE
        public IActionResult Create()
        {
            return View();
        }

        // SAVE STUDENT
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _repository.AddStudent(student);

                _repository.Save();

                return RedirectToAction("Index");
            }

            return View(student);
        }
    }
}