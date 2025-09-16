
using Management_Hotel_2025.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

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

            var IdUser = User.FindFirst("IdUser")?.Value;
            var Id = Convert.ToInt32(IdUser);


            var amountString = HttpContext.Session.GetString("Amount");
            decimal amount = 0;
            if (!string.IsNullOrEmpty(amountString))
            {
                amount = Convert.ToDecimal(amountString);
            }
            var result = amount * 0.2m;


          int? IdRoom =  HttpContext.Session.GetInt32("IdRoom");


            if (response.Success)
            {

                var NewBooking = new Booking
                {
                    UserId = Id,
                    BookingDate = DateTime.Now,
                    BookingSource = Id == 0 ? "Walk" : "Website",
                    DepositAmount = amountString == null ? 0 : result,
                    TotalAmountBooking = Convert.ToDecimal(HttpContext.Session.GetString("Amount")),
                    Status = "Success",
                };
                _dbcontext.Bookings.Add(NewBooking);

               
                _dbcontext.SaveChanges();

                int idBooking = NewBooking.BookingId;  // booking id  vừa tạo xong


                var NewbookingDetail = new BookingDetail
                {
                    BookingId = idBooking,
                    RoomId = IdRoom.Value,
                    CheckInDate = Convert.ToDateTime(HttpContext.Session.GetString("StartDate")),
                    CheckOutDate = Convert.ToDateTime(HttpContext.Session.GetString("EndDate")),
                };

                _dbcontext.BookingDetails.Add(NewbookingDetail);
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

        public IActionResult InformationBooking(string NameRoom, decimal Amount, int IdRoom)
        {

            HttpContext.Session.SetString("NameRoom", NameRoom);
            HttpContext.Session.SetString("Amount", Amount.ToString());
            HttpContext.Session.SetInt32("IdRoom", IdRoom);

    
            return View();
        }

        public IActionResult ResultPayment()
        {
            return View();
        }


    }
}
