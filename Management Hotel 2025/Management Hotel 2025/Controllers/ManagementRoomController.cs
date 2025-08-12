using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Controllers
{
    public class ManagementRoomController : Controller
    {
        [Authorize(Roles = "Admin,Manager,staff")]
        public IActionResult ViewListRoom()
        {
            return View();
        }

        public IActionResult ViewDetailRoom()
        {
            return View();
        }
    }
}
