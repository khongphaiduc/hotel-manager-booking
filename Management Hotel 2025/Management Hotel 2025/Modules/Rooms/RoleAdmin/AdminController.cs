using Management_Hotel_2025.Modules.Rooms.RoleAdmin.AdminServices;
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
        public IActionResult AdminManagementRoom()
        {
            return View();
        }



        [Authorize(Roles = "Admin")]
        [Route("serveralroom")]

        public IActionResult AdminHomePage()
        {
            var totalList = _iadmin.ViewListRoom();
            return View(totalList);
        }

    }
}
