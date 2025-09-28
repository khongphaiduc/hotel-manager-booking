using Management_Hotel_2025.ViewModel;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public interface IManagementRoom
    {

        public void CreateRoom();
        public void UpdateRoom();
        public void DeleteRoom();
        public Task<PaginatedResult<ViewRoomModel>> ViewListRoom(string option, int PageCurrent, int NumerItemOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person, string? StartDate, string? EndDate);
        public void ViewDetailRoom();

    }
}
