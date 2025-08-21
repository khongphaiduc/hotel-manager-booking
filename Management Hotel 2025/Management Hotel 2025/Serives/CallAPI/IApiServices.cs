using Management_Hotel_2025.ViewModel;

namespace Management_Hotel_2025.Serives.CallAPI
{
    public interface IApiServices
    {
        public Task<List<ViewRoomModel>> GetListRoomFromAPIAsync(string Token);

        public Task<ViewDetailRoom> ViewDetaiRoomAIPAsync(int id);

        public Task<PaginatedResult<ViewRoomModel>> ViewDetaiRoomAIPAsyncVer2(int PageCurrent, int NumerItemOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person);
    }
}
