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
        [HttpGet]
        public async Task<IActionResult> StaffViewListRoom(string option, int PageCurrent, int NumerItemOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person, string? StartDate, string? EndDate)
        {
            ViewBag.option = option;
            ViewBag.Floor = Floor;
            ViewBag.PageCurrent = PageCurrent;
            ViewBag.NumerItemOfPage = NumerItemOfPage;
            ViewBag.PriceMin = PriceMin;
            ViewBag.PriceMax = PriceMax;
            ViewBag.Person = Person;

            var Today = DateTime.Now;

            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                StartDate = Today.ToString("yyyy-MM-dd");
                EndDate = Today.AddDays(7).ToString("yyyy-MM-dd");
            }

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            var model = await _IManagementRoom.ViewListRoom("all", 1, 12, Floor, PriceMin, PriceMax, Person, StartDate, EndDate);

            return View(model);
        }

        // nhân viên tạo phiên đặt phòng  (cho case thằng khách hàng không booking trước )
        public IActionResult StaffBoookingRoom()
        {
            return View();
        }






        // check in 
        public IActionResult CheckInPassenger()
        {
            return View();
        }


        // check out
        public IActionResult CheckOutPassenger()
        {
            return View();
        }





    }
}
