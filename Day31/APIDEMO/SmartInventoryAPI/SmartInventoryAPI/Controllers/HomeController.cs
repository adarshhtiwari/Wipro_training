using Microsoft.AspNetCore.Mvc;

namespace SmartInventoryAPI_RoutingDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Welcome to Smart Inventory API Routing Demo!");
        }

        public IActionResult Reports()
        {
            return Content("This is the Reports page of Smart Inventory API Routing Demo!");
        }
    }
}