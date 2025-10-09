using Management_Hotel_2025.ViewModel;
using System.Text.Json;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public class ManagementViewListRoom : IManagementRoom
    {
        private readonly IConfiguration _confi;
        private readonly HttpClient _httpClient;

        public ManagementViewListRoom(IConfiguration configuration, HttpClient httpClient)
        {
            _confi = configuration;
            _httpClient = httpClient;
        }

        public void CreateRoom()
        {
            throw new NotImplementedException();
        }

        public void DeleteRoom()
        {
            throw new NotImplementedException();
        }

        public void UpdateRoom()
        {
            throw new NotImplementedException();
        }

        public void ViewDetailRoom()
        {
            throw new NotImplementedException();
        }

        // hiện thi danh sách phòng(advance) và phân trang
        public async Task<PaginatedResult<ViewRoomModel>> ViewListRoom(
     string option, int PageCurrent, int NumerItemOfPage,
     int? Floor, int? PriceMin, int? PriceMax, int? Person,
     string? StartDate, string? EndDate)
        {
            // xử lý giá trị mặc định
            PageCurrent = PageCurrent <= 0 ? 1 : PageCurrent;
            NumerItemOfPage = NumerItemOfPage <= 0 ? 10 : NumerItemOfPage;

            //string UrlApi = $"{_confi["ApiHotel:ManagementListRoom"]}" +
            //                $"?option={option}&PageCurrent={PageCurrent}&NumerItemOfPage={NumerItemOfPage}" +
            //                $"&Floor={Floor}&PriceMin={PriceMin}&PriceMax={PriceMax}" +
            //                $"&Person={Person}&StartDate={StartDate}&EndDate={EndDate}";

            var url = $"{_confi["ApiHotel:ViewListRoomByPaginationByType"]}?PageCurrent={PageCurrent}&NumerItemOfPage={NumerItemOfPage}&Floor={Floor}&PriceMin={PriceMin}&PriceMax={PriceMax}&Person={Person}&StartDate={StartDate}&EndDate={EndDate}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new PaginatedResult<ViewRoomModel>();

            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PaginatedResult<ViewRoomModel>>(result,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new PaginatedResult<ViewRoomModel>();
        }


    }
}
