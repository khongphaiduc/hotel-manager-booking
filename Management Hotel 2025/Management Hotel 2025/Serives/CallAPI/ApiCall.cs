using Management_Hotel_2025.ViewModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Management_Hotel_2025.Serives.CallAPI
{
    public class ApiCall : IApiServices
    {
        private readonly IConfiguration _IConfiguration;

        public ApiCall(IConfiguration configuration)
        {
            _IConfiguration = configuration;
        }

        // call API to get list of rooms
        public async Task<List<ViewRoomModel>> GetListRoomFromAPIAsync(string Token)
        {
            using (var client = new HttpClient())  //HttpClient là 1 class  HttpClient dùng để gửi HTTP request đi tới server khác hoặc cùng server. Nó giúp gọi API, nhận dữ liệu JSON, XML, text…

            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);   // thằng này sẽ ghi Token vào header để gửi đi đến Api
                var response = await client.GetAsync(_IConfiguration["ApiHotel:CallListRoom"]);  // url của api
                string result = await response.Content.ReadAsStringAsync();    // nội dung được trả về từ apis



                //     JsonSerializer.Deserialize<T>(string json , JsonSerializerOptions) dùng để chuyền chuỗi json thành model
                var rooms = JsonSerializer.Deserialize<List<ViewRoomModel>>(result,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return rooms ?? new List<ViewRoomModel>();
            }
        }


        // call API to get detail of room   
        public async Task<ViewDetailRoom> ViewDetaiRoomAIPAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://localhost:7236/api/getbooking/ViewDetailRoom/{id}";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    // Có thể log lỗi lại để dễ debug
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API call failed ({response.StatusCode}): {error}");
                }

                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ViewDetailRoom>(result,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? new ViewDetailRoom();
            }
        }

    }
}
