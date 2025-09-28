using Management_Hotel_2025.Modules.Rooms.RoomService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Modules.Rooms.RoomsController
{
    public class StaffManagementRoomController : Controller
    {
        private readonly IManagementRoom _IManagementRoom;

        public StaffManagementRoomController(IManagementRoom managementRoom)
        {
            _IManagementRoom = managementRoom;
        }


        //  xem danh sách phòng 
        [Authorize(Roles = "Staff,Admin")]
        public async Task<IActionResult> StaffViewListRoom(string option, int PageCurrent, int NumerItemOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person, string? StartDate, string? EndDate)
        {

            var model = await _IManagementRoom.ViewListRoom("all", 1, 12, null, null, null, null, null, null);

            return View(model);
        }
    }
}
