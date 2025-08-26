using Management_Hotel_2025.Serives.VNPay;
using Management_Hotel_2025.ViewModel.VNPay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        public PaymentController(IVnPayService vnPayService)
        {

            _vnPayService = vnPayService;
        }

        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var s = new PaymentInformationModel()
            {
                OrderType = HttpContext.Session.GetString("OrderType"),
                Amount = Convert.ToDouble(HttpContext.Session.GetString("Amount")),
                OrderDescription = HttpContext.Session.GetString("OrderDescription"),
                Name = HttpContext.Session.GetString("Name")
            };


            var url = _vnPayService.CreatePaymentUrl(s, HttpContext);
            Console.WriteLine("URL VNPAY: " + url);

            return Redirect(url);
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin,Staff")]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }

        [Authorize(Roles = "User,Admin,Staff")]
        public IActionResult InformationBooking(string NameRoom, decimal Amount)
        {
            HttpContext.Session.SetString("OrderType", "other");
            HttpContext.Session.SetString("Amount", Amount.ToString());
            HttpContext.Session.SetString("OrderDescription", "Đặt Cọc Phòng");
            HttpContext.Session.SetString("Name", User.Identity?.Name ?? "Ẩn Danh");
            HttpContext.Session.SetString("NameRoom", NameRoom);
           
            return View();
        }

    }
}
