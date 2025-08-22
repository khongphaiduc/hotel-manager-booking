using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Controllers
{
    public class CartController : Controller
    {
        public IActionResult ViewCart()
        {
            return View();
        }
    }
}
