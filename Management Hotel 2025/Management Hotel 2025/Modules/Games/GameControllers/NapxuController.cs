using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayOS;
using PayOS.Models.Webhooks;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using PayOS.Models.V2.PaymentRequests;
namespace Management_Hotel_2025.Modules.Games.GameControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NapxuController : ControllerBase
    {

        private readonly PayOSClient _payOS;
        private readonly ILogger<GameController> _logger;

        public NapxuController(ILogger<GameController> logger, PayOSClient payOS)
        {
            _logger = logger;
            _payOS = payOS;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromBody] Webhook webhook)
        {
            // Verify webhook từ PayOS
            var webhookData = await _payOS.Webhooks.VerifyAsync(webhook);

            if (webhookData.Code == "00") // thanh toán thành công
            {
                // Lấy thông tin thanh toán
                var orderCode = webhookData.OrderCode;
                var amount = webhookData.Amount;
                var reference = webhookData.Reference;
                var transactionTime = webhookData.TransactionDateTime;

                Console.WriteLine($"Charged succes");
                Console.WriteLine($"code order: {orderCode}");
                Console.WriteLine($"amunt: {amount}");
                Console.WriteLine($"code reference: {reference}");
                Console.WriteLine($"Time: {transactionTime}");

                // TODO: cập nhật database user, cộng coin, lưu lịch sử, v.v.
                return Ok();
            }
            else
            {
                Console.WriteLine($"Thanh toán thất bại: {webhookData.Code} - {webhookData.Description2}");
                return BadRequest();
            }
        }

    }

}
