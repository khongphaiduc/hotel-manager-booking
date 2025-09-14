using Management_Hotel_2025.ViewModel;

namespace Management_Hotel_2025.Serives.CallAPI
{
    public interface IApiServices
    {

        // View Detail Room
        public Task<ViewDetailRoom> ViewDetaiRoomAIPAsync(int id);

        // Search Room Advance and Pagination
        public Task<PaginatedResult<ViewRoomModel>> ViewListRoomAPIAsync(int PageCurrent, int NumerItemOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person,string StartDate,string EndDate);

    }
}
