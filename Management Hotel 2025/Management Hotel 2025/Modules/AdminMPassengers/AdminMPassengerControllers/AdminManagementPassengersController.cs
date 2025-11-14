
using Management_Hotel_2025.Modules.AdminMPassengers.MPassengersServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Threading.Tasks;

namespace Management_Hotel_2025.Modules.AdminMPassengers.AdminMPassengerControllers
{

    [Route("admin")]
    public class AdminManagementPassengersController : Controller
    {
        private readonly IConfiguration _Iconfi;
        public string apiPassengers = "";
        private readonly IAdminMPassengers _IadminMPassgers;
        public AdminManagementPassengersController(IConfiguration configuration, IAdminMPassengers admin)
        {
            _IadminMPassgers = admin;
            _Iconfi = configuration;
            apiPassengers = _Iconfi["ApiHotel:PassengerInfo"];
        }

        // xem danh sách khách hangh
        [HttpGet("passengers")]
        public async Task<IActionResult> ViewListPassenger()
        {

            var listPassengers = await _IadminMPassgers.GetListViewPassengers();

            return View(listPassengers);
        }

        // xem chi tiết hành khách
        [HttpGet("passengers/{codePassenger}")]
        public async Task<IActionResult> GetPassengersInfo(string codePassenger)
        {

            string url = apiPassengers + "/" + codePassenger;

            try
            {

                using (var httpclient = new HttpClient())
                {
                    var response = await httpclient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();

                        var passengerList = Newtonsoft.Json.JsonConvert.DeserializeObject<PassengerDetail>(data);

                        return View(passengerList);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Error retrieving passengers");
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
