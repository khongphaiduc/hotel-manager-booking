

using Management_Hotel_2025.Modules.Notifications.NotificationsSevices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Mydata.Models;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Management_Hotel_2025.Modules.Payment.PaymentControllers
{
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly ManagermentHotelContext _dbcontext;
        private readonly INotifications _notifications;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IVnPayService vnPayService, ManagermentHotelContext managermentHotelContext, INotifications notifications, ILogger<PaymentController> logger)
        {

            _vnPayService = vnPayService;
            _dbcontext = managermentHotelContext;
            _notifications = notifications;
            _logger = logger;
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

            // id phòng đang booking
            int? IdRoom = HttpContext.Session.GetInt32("IdRoom");


            decimal TotalRoom = Convert.ToDecimal(HttpContext.Session.GetString("TotalRoom"));

            decimal DepositAmount = Convert.ToDecimal(HttpContext.Session.GetString("DepositAmount"));

            if (response.Success)
            {

                var NewBooking = new Booking
                {
                    UserId = Id,
                    BookingDate = DateTime.Now,
                    BookingSource = Id == 0 ? "Walk" : "Website",
                    DepositAmount = DepositAmount,
                    TotalAmountBooking = TotalRoom,
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

            string testmail = User.FindFirst(ClaimTypes.Email)?.Value;
            _logger.LogInformation($"mail ở info  :{testmail}");

            HttpContext.Session.SetString("NameRoom", NameRoom);
            HttpContext.Session.SetString("Amount", Amount.ToString());
            HttpContext.Session.SetInt32("IdRoom", IdRoom);


            // số ngày  ở của hành khách
            DateTime ExpectedChechInTime = Convert.ToDateTime(HttpContext.Session.GetString("StartDate"));
            DateTime ExpectedCheckOutTime = Convert.ToDateTime(HttpContext.Session.GetString("EndDate"));

            TimeSpan NumberDate = ExpectedCheckOutTime - ExpectedChechInTime;

            int Days = NumberDate.Days;
            // giá phòng  
            decimal PriceRoom = Convert.ToDecimal(HttpContext.Session.GetString("Amount"));

            //tổng số tiền phòng 

            decimal TotalRoom = Days * PriceRoom;

            //số tiền cọc
            decimal DepositAmount = (Days * PriceRoom) * 0.2m;



            //
            HttpContext.Session.SetString("DepositAmount", DepositAmount.ToString());
            HttpContext.Session.SetString("TotalRoom", TotalRoom.ToString());
            HttpContext.Session.SetString("TotalDays", Days.ToString());
            return View();
        }

        public async Task<IActionResult> ResultPayment()
        {
            string deposit = HttpContext.Session.GetString("DepositAmount");
            string formatted = "";
            if (decimal.TryParse(deposit, out decimal depositAmount))
            {
                formatted = depositAmount.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
                // formatted = "50.000 ₫"
            }

            HttpContext.Session.GetString("TotalRoom");
            HttpContext.Session.GetString("TotalDays");

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var name = User.FindFirst("FullName")?.Value;
            var phone = User.FindFirst("PhoneNumber")?.Value;
            var roomType = HttpContext.Session.GetString("NameRoom");
            var checkIn = HttpContext.Session.GetString("StartDate");
            var checkOut = HttpContext.Session.GetString("EndDate");
            var guestCount = HttpContext.Session.GetString("GuestCount");
            var totalPrice = HttpContext.Session.GetString("TotalRoom");
            string Content = $@"
<p>Kính gửi Quý khách,</p>

<p>Chúng tôi xin trân trọng cảm ơn Quý khách đã tin tưởng và lựa chọn dịch vụ của 
<b>Khách sạn Luxury Trung Đức</b>.</p>

<p>Chúng tôi xin thông báo rằng việc đặt phòng của Quý khách đã <b>THÀNH CÔNG</b> với các thông tin sau:</p>

<ul>
  <li><b>Họ và tên:</b> {name}</li>
  <li><b>Số điện thoại:</b> {phone}</li>
  <li><b>Email:</b> {email}</li>
  <li><b>Loại phòng:</b> {roomType}</li>
  <li><b>Ngày nhận phòng:</b> {checkIn}</li>
  <li><b>Ngày trả phòng:</b> {checkOut}</li>
  <li><b>Số lượng khách:</b> {guestCount}</li>
  <li><b>Số tiền đã đạt cọc là :</b> {formatted}</li>
</ul>

<p>Quý khách vui lòng có mặt tại khách sạn vào ngày nhận phòng và mang theo giấy tờ tùy thân để hoàn tất thủ tục check-in.</p>
<p>Trong trường hợp nếu quý khách không check in phòng trong ngày nhận thì phòng sẽ được hủy theo quy định của khách sạn .</p>
<p>Nếu Quý khách có bất kỳ yêu cầu đặc biệt hoặc cần hỗ trợ thêm, xin vui lòng liên hệ với chúng tôi qua:<br>
📞 Hotline: 033333333<br>
📧 Email: [hotelluxurytrungduc@gmail.com]</p>

<p>Một lần nữa, xin cảm ơn Quý khách đã lựa chọn <b>Khách sạn Luxury Trung Đức</b>.<br>
Chúng tôi hân hạnh được đón tiếp Quý khách!</p>

<p>Trân trọng,<br>
<b>Khách sạn Luxury Trung Đức</b></p>
";


            var reuslt = await _notifications.SendBookingSuccessNotification(email, "Xác nhận đặt phòng thành công - Khách sạn Luxury Trung Đức", Content);

            var s = reuslt == true ? "Success" : "Fail";
            _logger.LogInformation($"Enail : {email} :{s}");
            _logger.LogInformation($"Send email to {email} :{s}");
            return View();
        }


    }
}
