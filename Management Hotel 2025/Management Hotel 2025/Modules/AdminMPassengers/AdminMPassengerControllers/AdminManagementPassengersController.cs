using Management_Hotel_2025.Modules.AdminMPassengers.MPassengersServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Management_Hotel_2025.Modules.AdminMPassengers.AdminMPassengerControllers
{

    [Route("admin")]
    public class AdminManagementPassengersController : Controller
    {
        private readonly IAdminMPassengers _IadminMPassgers;

        public AdminManagementPassengersController(IAdminMPassengers admin)
        {
            _IadminMPassgers = admin;
        }

        [Route("passengers")]
        public async Task<IActionResult> ViewListPassenger()
        {

            var listPassengers = await _IadminMPassgers.GetListViewPassengers();

            return View(listPassengers);
        }
    }
}
