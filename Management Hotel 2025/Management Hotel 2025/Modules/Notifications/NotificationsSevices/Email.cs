
using System.Net.Mail;
using System.Net;

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

    }
}
