
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace Management_Hotel_2025.Modules.Notifications.NotificationsSevices
{
    public class Email : INotifications
    {
        public Task<bool> SendBookingFailureNotification(string message, string recipient)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendBookingSuccessNotification(string toEmail, string subject, string body)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("hotelluxurytrungduc@gmail.com", "ykbg blmo tqxy hrld");
                    smtp.EnableSsl = true;

                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress("hotelluxurytrungduc@gmail.com", "Hotel Management");
                        message.To.Add(toEmail);
                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;

                        await smtp.SendMailAsync(message);
                    }
                }

                Console.WriteLine("Email sent successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                return false;
            }
        }

        //gửi mã qr kèm theo email
        public async Task<bool> SendBookingSuccessNotification(string toEmail, string subject, string body, byte[] qrCodeBytes)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("hotelluxurytrungduc@gmail.com", "ykbg blmo tqxy hrld"); // App password Gmail
                    smtp.EnableSsl = true;

                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress("hotelluxurytrungduc@gmail.com", "Khách sạn Luxury Trung Đức");
                        message.To.Add(toEmail);
                        message.Subject = subject;
                        message.IsBodyHtml = true;

                        // 1️⃣ Tạo LinkedResource từ byte[] QR
                        LinkedResource qrImage = new LinkedResource(new MemoryStream(qrCodeBytes), MediaTypeNames.Image.Jpeg);
                        qrImage.ContentId = "QrCodeImage";
                        qrImage.TransferEncoding = TransferEncoding.Base64;

                        // 2️⃣ Tạo nội dung HTML có ảnh nhúng
                        string htmlBody = $@"
                {body}
                <div style='text-align:center;margin-top:10px;'>
                    <img src='cid:QrCodeImage' alt='QR Code đặt phòng' width='180' height='180' />
                </div>

<p>Nếu Quý khách có bất kỳ yêu cầu đặc biệt hoặc cần hỗ trợ thêm, xin vui lòng liên hệ với chúng tôi qua:<br>
📞 Hotline: 033333333<br>
📧 Email: hotelluxurytrungduc@gmail.com</p>

<p>Một lần nữa, xin cảm ơn Quý khách đã lựa chọn <b>Khách sạn Luxury Trung Đức</b>.<br>
Chúng tôi hân hạnh được đón tiếp Quý khách!</p>

<p>Trân trọng,<br>
<b>Khách sạn Luxury Trung Đức</b></p>
";

                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
                        htmlView.LinkedResources.Add(qrImage);

                        message.AlternateViews.Add(htmlView);

                        await smtp.SendMailAsync(message);
                    }
                }

                Console.WriteLine("✅ Email sent successfully with QR code!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error sending email: " + ex.Message);
                return false;
            }
        }

    }
}
