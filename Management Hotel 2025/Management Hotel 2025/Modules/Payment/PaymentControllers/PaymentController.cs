
using Management_Hotel_2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Modules.Payment.PaymentControllers
{
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly ManagermentHotelContext _dbcontext;

        public PaymentController(IVnPayService vnPayService, ManagermentHotelContext managermentHotelContext)
        {

            _vnPayService = vnPayService;
            _dbcontext = managermentHotelContext;
        }

        [HttpPost]                
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }


        [HttpGet]
        public IActionResult PaymentCallbackVnpay()
        {
            PaymentResponseModel response = _vnPayService.PaymentExecute(Request.Query);
            int IdUser = HttpContext.Session.GetInt32("UserId") ?? 0;

            if (response.Success)
            {
                _dbcontext.Bookings.Add(new Booking
                {
                   
                    BookingDate = DateTime.Now,
                    BookingSource= IdUser==0?"WalkIn":"Website",
                    DepositAmount = Convert.ToDecimal(HttpContext.Session.GetString("Amount")),
                    Status = "Success",
                });

                _dbcontext.SaveChanges();
                return RedirectToAction("ResultPayment", "Payment", new { success = true });
            }
            else
            {
                // Xử lý khi thanh toán thất bại
                // Ví dụ: Hiển thị thông báo lỗi hoặc chuyển hướng người dùng đến trang lỗi
                return BadRequest("Payment failed. Please try again.");
            }          
        }

        public IActionResult InformationBooking(string NameRoom, decimal Amount)
        {

            HttpContext.Session.SetString("NameRoom", NameRoom);
            HttpContext.Session.SetString("Amount", Amount.ToString());
         
            return View();
        }

        public IActionResult ResultPayment()
        {       
            return View();
        }


    }
}
