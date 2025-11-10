using Management_Hotel_2025.Modules.AmentityModules.AmentityServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Management_Hotel_2025.Modules.AmentityModules.AmentityControllers
{

    [Route("admin")]
    public class ManagementAmenityController : Controller
    {

        [HttpGet("amenity")]
        public async Task<IActionResult> ViewListAmentity()
        {
            string url = "https://localhost:7236/api/amenity";

            using (var httpclient = new HttpClient())
            {

                var response = await httpclient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    var amentity = JsonConvert.DeserializeObject<List<MyAmenity>>(jsonString);

                    return View(amentity);
                }
                else
                {
                    return View(new List<MyAmenity>());
                }

            }

        }
    }
}
