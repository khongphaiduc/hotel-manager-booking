using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Controllers
{
    public class ManagementRoomController : Controller
    {
        [Authorize(Roles = "Admin,User,staff")]
        public IActionResult ViewListRoom()
        {
            HttpContext.Response.Headers.Append("Phamtrungduc", "DEptria");
            return View();
        }

        public IActionResult ViewDetailRoom()
        {
            return View();
        }
    }
}
