using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ValidationDemoApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult REGISTER()
        {
            return View();
        }
    }
    public IActionResult: Controller


   public IActionResult Register(Student model)
   {
        if(!ModelState.IsValid) {
            return View(model);
        }

        ViewBag.Message = "Registration successful!";
        return View("Success");
    }

}

