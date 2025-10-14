using Management_Hotel_2025.Modules.Rooms.RoomService;
using Management_Hotel_2025.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Modules.Rooms.RoomsController
{
    public class StaffManagementRoomController : Controller
    {
        private readonly IManagementRoom _IManagementRoom;


        public List<Passengers> PassengersList { get; set; } = new List<Passengers>();
        public StaffManagementRoomController(IManagementRoom managementRoom)
        {
            _IManagementRoom = managementRoom;
        }


        [Authorize(Roles = "Staff,Admin")]
        [HttpGet]
        public async Task<IActionResult> StaffViewListRoom(string option, int? Floor, DateTime StartDate, DateTime EndDate)
        {
            if (StartDate == DateTime.MinValue || EndDate == DateTime.MinValue)
            {
                var Today = DateTime.Now;
                StartDate = Today;
                EndDate = Today.AddDays(7);
            }

            // 👇 Gán sau khi đã xử lý mặc định
            ViewBag.option = option;
            ViewBag.Floor = Floor;
            ViewBag.StartDate = StartDate.ToString("yyyy-MM-dd"); // format cho input date
            ViewBag.EndDate = EndDate.ToString("yyyy-MM-dd");

            var ListRoom = await _IManagementRoom.FilterRoom(option, Floor, StartDate, EndDate);

            return View(ListRoom);
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpGet]
        public async Task<IActionResult> StaffSearchByIdRoom(string IdRoom)
        {
            if (string.IsNullOrEmpty(IdRoom))
            {
                return RedirectToAction("StaffViewListRoom");
            }
            else
            {
                List<ViewRoomModel> list = new List<ViewRoomModel>();
                list.Add(await _IManagementRoom.FilterByIdRoom(IdRoom));
                return View("StaffViewListRoom", list);
            }

        }


        public IActionResult StaffViewDetailRoom(string IdRoom)
        {
            var Room = _IManagementRoom.FilterByIdRoom(IdRoom).Result;
            return View(Room);
        }




        // nhân viên tạo phiên đặt phòng  (cho case thằng khách hàng không booking trước )
        public IActionResult StaffBoookingRoom()
        {
            return View();
        }



        // xem lịch đặt phòng của phòng
        public IActionResult ViewCalenderBookingOfRoom(int IdRoom,string NumberRoom)
        {
            ViewBag.NumberRoom = NumberRoom;
            var calender = _IManagementRoom.GetListDateBookingOfRoom(IdRoom);

            return View(calender);
        }


        // check in 
        public IActionResult CheckInPassenger(Passengers passengers)
        {

            PassengersList.Add(passengers);

            return View(PassengersList);
        }


        // check out
        public IActionResult CheckOutPassenger()
        {
            return View();
        }





    }
}
