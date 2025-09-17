
using API_BookingHotel.Modules.Payment.VNPay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mydata.Models;
using System.Text.Json;

namespace API_BookingHotel.Modules.Payment.PaymensControllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        private readonly IVnPayService _vnPayService;
        private readonly ManagermentHotelContext _httcontext;

        public PaymentsController(IVnPayService vnPayService, ManagermentHotelContext httcontext)
        {
            _vnPayService = vnPayService;
            _httcontext = httcontext;
        }



        // tạo payment 
        [HttpPost("vnpay")]
        //[Authorize]
        public IActionResult CreatePaymentUrlVnpay([FromBody]PaymentInformationModel model)
        {
            var s = new PaymentInformationModel()
            {
                OrderType = model.OrderType,
                Amount = model.Amount,
                OrderDescription = model.OrderDescription,
                Name = model.Name
            };

            string url = _vnPayService.CreatePaymentUrl(s, HttpContext);
            return Redirect(url);
        }


        // thành toán callback
        [HttpGet("vnpay")]
        //[Authorize]
        public IActionResult PaymentCallbackVnpay()
        {
            PaymentResponseModel response = _vnPayService.PaymentExecute(Request.Query);

            int idUser = _httcontext.Users.FirstOrDefault(u => u.Username == User.Identity.Name)?.UserId ?? 0;

            if (response.Success)
            {
                //var newbooking = new Booking()
                //{
                //    UserId = idUser,   // Name  = Email
                //    BookingDate = DateTime.Now,
                //    BookingSource = idUser != 0 ? "Website " : "At Hotel",
                //    Status = "Đã Đặt Cọc",
                //    DepositAmount = response.Amount,
                //    //TotalAmountBooking = (decimal)Convert.ToDecimal(response.OrderDescription)

                //};
                return Ok(response);
            }
            else
            {
                // Xử lý logic khi thanh toán thất bại
                // Ví dụ: Ghi log lỗi hoặc thông báo cho người dùng
            }

            return Ok(new {status = "payment fail"});

        }


    }
}
