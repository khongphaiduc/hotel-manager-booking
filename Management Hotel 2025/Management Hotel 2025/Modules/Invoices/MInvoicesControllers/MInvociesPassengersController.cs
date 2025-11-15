using Management_Hotel_2025.Modules.AdminMPassengers.AdminMPassengerControllers;
using Management_Hotel_2025.Modules.Invoices.InvocieModels;
using Microsoft.AspNetCore.Mvc;



namespace Management_Hotel_2025.Modules.Invoices.MInvoicesControllers
{
    [Route("admin")]
    public class MInvociesPassengersController : Controller
    {
        private readonly IConfiguration _Iconfig;
        private readonly string apiBaseUrl;
        private readonly ILogger<MInvociesPassengersController> _Ilogger;

        public MInvociesPassengersController(IConfiguration configuration, ILogger<MInvociesPassengersController> logger)
        {
            _Iconfig = configuration;
            apiBaseUrl = _Iconfig["ApiHotel:PassengerInvoice"];
            _Ilogger = logger;
        }


        // lấy danh sách hóa đơn
        [HttpGet("invoice")]
        public async Task<IActionResult> GetlistInvoicesPassengers()
        {
            try
            {
                using (var httpclient = new HttpClient())
                {
                    var response = await httpclient.GetAsync(apiBaseUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var ListInvoicesPassengers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvoicesViewModel>>(data);

                        return View(ListInvoicesPassengers);
                    }
                    else
                    {
                        return View(new List<InvoicesViewModel>());
                    }
                }
            }
            catch (Exception s)
            {
                _Ilogger.LogInformation($"Bug : {s.Message}");
                throw;
            }

        }
    }
}
