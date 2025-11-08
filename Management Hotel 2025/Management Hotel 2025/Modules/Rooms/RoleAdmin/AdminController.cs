using Management_Hotel_2025.Modules.Rooms.RoleAdmin.AdminServices;
using Management_Hotel_2025.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Modules.Rooms.RoleAdmin
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IAdminManagement _iadmin;

        public AdminController(IAdminManagement iadmin)
        {
            _iadmin = iadmin;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("room")]
        public IActionResult AdminManagementRoom()
        {
            var listroom = _iadmin.ViewTypeRoom();

            return View(listroom);
        }



        [Authorize(Roles = "Admin")]
        [Route("serveralroom")]
        public IActionResult AdminHomePage()
        {
            var totalList = _iadmin.ViewListRoom();
            return View(totalList);
        }


        [Authorize(Roles = "Admin")]
        [Route("room/{idRoom}")]
        [HttpGet]
        public IActionResult AdjustRoom(int idRoom)
        {
            var roomDetails = _iadmin.GetRoomDetails(idRoom);

            return View(roomDetails);
        }


        [Authorize(Roles = "Admin")]
        [Route("room/{idRoom}")]
        [HttpPost]
        public IActionResult AdjustRoom(AdJustRoom roomjust)
        {

            //cập nhật lại thông tin cơ bản của room
            bool result = _iadmin.AdjustRoom(roomjust);

            if (result)
            {
                return Ok(new { success = true, message = "Cập nhật phòng thành công!" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Cập nhật phòng thất bại!" });
            }
        }

    }
}
