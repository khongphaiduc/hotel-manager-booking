
using Management_Hotel_2025.Modules.Rooms.ManagementRoom;
using Management_Hotel_2025.Modules.Rooms.RoomService;
using Management_Hotel_2025.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.WebSockets;


namespace Management_Hotel_2025.Modules.Rooms.RoomsController
{
    public class StaffManagementRoomController : Controller
    {
        private readonly IManagementRoom _IManagementRoom;
        private readonly IManagementBooking _IManagementBooking;

        public List<Passengers> PassengersList { get; set; } = new List<Passengers>();
        public StaffManagementRoomController(IManagementRoom managementRoom, IManagementBooking managementBooking)
        {
            _IManagementRoom = managementRoom;
            _IManagementBooking = managementBooking;
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

            if (option == null)
            {
                option = "all";
            }


            // 👇 Gán sau khi đã xử lý mặc định
            ViewBag.option = option;
            ViewBag.Floor = Floor;
            ViewBag.StartDate = StartDate.ToString("yyyy-MM-dd"); // format cho input date
            ViewBag.EndDate = EndDate.ToString("yyyy-MM-dd");

            var ListRoom = await _IManagementRoom.FilterRoom(option, Floor, StartDate, EndDate);

            return View(ListRoom);
        }
        // search room
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
        public IActionResult ViewCalenderBookingOfRoom(int IdRoom, string NumberRoom)
        {
            ViewBag.NumberRoom = NumberRoom;
            var calender = _IManagementRoom.GetListDateBookingOfRoom(IdRoom);

            return View(calender);
        }


        // xem sơ đồ phòng 


        public IActionResult ViewMapOfRoom()
        {
            return View(_IManagementRoom.getListMapRoomToDay());
        }







        // check in 
        public IActionResult CheckInPassengers()
        {


            return View();
        }


        // check out
        public IActionResult CheckOutPassenger()
        {
            return View();
        }


        // xem danh sách booking theo ngày hoặc tìm kiếm theo mã booking

        public IActionResult BookingsView(string search, DateTime? DateStart, DateTime? EndDate)
        {
            // Nếu không có ngày được chọn => mặc định 1 tháng trước đến 1 tháng sau
            DateTime start = DateStart ?? DateTime.Now.AddMonths(-1);
            DateTime end = EndDate ?? DateTime.Now.AddMonths(1);

            ViewBag.DateStart = start.ToString("yyyy-MM-dd");
            ViewBag.EndDate = end.ToString("yyyy-MM-dd");

            List<BookingItem> list;

            if (!string.IsNullOrEmpty(search))
            {
                list = _IManagementBooking.GetListBooking(search);
            }
            else
            {
                list = _IManagementBooking.GetListBooking(start, end);
            }

            return View(list);
        }

        public IActionResult ViewDetailBooking()
        {
          
            return View();
        }

    }
}
