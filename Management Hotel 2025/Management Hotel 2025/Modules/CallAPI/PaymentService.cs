using System.Text.Json;
using System.Text;

namespace Management_Hotel_2025.Modules.CallAPI
{
    public class PaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymentService(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }


        /*public async void CreatePaymentByVnpay(PaymentInformationModel model)
        {

            var json = JsonSerializer.Serialize(model);    // chuyển thành json 
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            *//*
             new StringContent(json, Encoding.UTF8, "application/json")

            StringContent là một class trong .NET dùng để gói chuỗi (string) thành HTTP body content.

            Tham số 1: json → chính là chuỗi JSON bạn muốn gửi đi.

            Tham số 2: Encoding.UTF8 → mã hóa chuỗi JSON theo chuẩn UTF-8 (tránh lỗi tiếng Việt, ký tự đặc biệt).

            Tham số 3: "application/json" → đây là Content-Type header của HTTP request, báo cho server biết body này là JSON*//*

            var response = await _httpClient.PostAsync(_configuration["ApiHotel:VnPayPayment"],content);
          
            if (response.IsSuccessStatusCode)   // mã trạng thái http trong khoảng 200-299 thì return về  true và ngược lại 
            {
                var paymentUrl = await response.Content.ReadAsStringAsync();

            }
        }*/

    }
}
