using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Modules.Rooms.RoomsController
{
    public class RoomsOfUserController : Controller
    {

        [Authorize(Roles ="User")]
        public IActionResult ViewListRooms()
        {
            return View();
        }
    }
}
