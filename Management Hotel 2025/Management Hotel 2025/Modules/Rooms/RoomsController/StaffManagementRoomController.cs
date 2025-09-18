using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Modules.Rooms.RoomsController
{
    public class StaffManagementRoomController : Controller
    {
        [Authorize(Roles ="Staff,Admin")]
        public IActionResult  StaffViewListRoom()
        {
            return View();
        }
    }
}
