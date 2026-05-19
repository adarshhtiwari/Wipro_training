using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Controllers
{
    [Route("restaurant")]
    public class RestaurantController : Controller
    {
        // US-101: Clean URL -> /restaurant/menu
        [Route("menu")]
        public IActionResult Menu()
        {
            return Content("this is restaurant Menu Page");
        }

        // US-103: Attribute routing -> /restaurant/details/5
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            return Content("Restaurant Details ID: " + id);
        }

        // US-104: Route constraint -> only valid for int, e.g. /restaurant/details/10
        [Route("details/{id:int}")]
        public IActionResult DetailsConstrained(int id)
        {
            return Content("Restaurant ID: " + id);
        }
    }
}