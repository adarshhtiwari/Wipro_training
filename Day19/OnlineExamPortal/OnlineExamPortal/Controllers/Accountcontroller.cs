using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username)
    {
        HttpContext.Session.SetString("User", username);
        return RedirectToAction("Dashboard");
    }

    public IActionResult Dashboard()
    {
        var user = HttpContext.Session.GetString("User");
        return Content("Welcome " + user);
    }
}