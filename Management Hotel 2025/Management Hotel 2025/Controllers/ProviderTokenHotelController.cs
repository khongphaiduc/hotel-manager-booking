using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Controllers
{
    public class ProviderTokenHotelController : Controller
    {

        [Authorize(Roles ="User,Admin,Staff")]
        public IActionResult ViewGetInfo()
        {
            return View();
        }
    }
}
