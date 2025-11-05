
using Microsoft.AspNetCore.Mvc;
using PayOS;
using PayOS.Exceptions;
using PayOS.Models.V2.PaymentRequests;

namespace Management_Hotel_2025.Modules.Games.GameControllers
{
    [Route("game")]
    public class GameController : Controller
    {

        private readonly ILogger<GameController> _logger;
        private readonly PayOSClient _payOSClient;
        public GameController(ILogger<GameController> logger, PayOSClient payOS)
        {
            _logger = logger;

            _payOSClient = new PayOSClient(new PayOSOptions
            {
                ClientId = "xemconcak",
                ApiKey = "xemconcak",
                ChecksumKey = "xemconcak"
            });

        }

        [HttpGet("createpayment")]
        public async Task<IActionResult> CreatePayment()
        {
            var paymentRequest = new CreatePaymentLinkRequest
            {
                OrderCode = long.Parse(DateTime.Now.ToString("ddHHmmss")),
                Amount = 10111,
                Description = "TEST",
                ReturnUrl = "https://wastingly-preroyal-leonardo.ngrok-free.dev/game/success",
                CancelUrl = "https://wastingly-preroyal-leonardo.ngrok-free.dev/game/cancel"
            };

            try
            {
                var paymentLink = await _payOSClient.PaymentRequests.CreateAsync(paymentRequest);
                return Redirect(paymentLink.CheckoutUrl);
            }
            catch (ApiException ex)
            {
                return Content($"API Error: {ex.Message}, Code: {ex.ErrorCode}");
            }
            catch (PayOSException ex)
            {
                return Content($"PayOS Error: {ex.Message}");
            }
        }


        [HttpPost("createpaymentver1")]
        public async Task<IActionResult> Indexs()
        {


            var clientId = "xemconcak";
            var apiKey = "xemconcak";
            var checksumKey = "xemconcak";

            var client = new PayOSClient(clientId, apiKey, checksumKey);

            try
            {
                List<PaymentLinkItem> items = [new PaymentLinkItem
            {
                Name = "my tom",
                Price = 1000,
                Quantity = 2
            }];
                var paymentData = new CreatePaymentLinkRequest
                {
                    OrderCode = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                    Amount = 2000,
                    Description = "payment",
                    ReturnUrl = "https://wastingly-preroyal-leonardo.ngrok-free.dev/game/success",
                    CancelUrl = "https://wastingly-preroyal-leonardo.ngrok-free.dev/game/cancel",
                    Items = items
                };
                var createPaymentLinkResponse = await client.PaymentRequests.CreateAsync(paymentData);
                Console.WriteLine($"Checkout URL: {createPaymentLinkResponse.CheckoutUrl}");
                var paymentLink = await client.PaymentRequests.GetAsync(createPaymentLinkResponse.PaymentLinkId);
                return Redirect(createPaymentLinkResponse.CheckoutUrl);
                // paymentLink = await client.PaymentRequests.CancelAsync(createPaymentLinkResponse.PaymentLinkId, "change my mind"); // Uncomment this line for cancel payment link
            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpGet("success")]
        public IActionResult Success()
        {
            return Ok(new { success = true, message = "Thanh toán thành công" });
        }

        [HttpGet("cancel")]
        public IActionResult Cancel()
        {
            return Ok(new { success = false, message = "Thanh toán bị hủy" });
        }

    }
}
