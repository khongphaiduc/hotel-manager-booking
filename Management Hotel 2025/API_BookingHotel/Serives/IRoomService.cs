using API_BookingHotel.ViewModels;

namespace API_BookingHotel.Serives
{
    public interface IRoomService
    {

        public Task<List<ViewRoom>> GetListRoomHotelAsync(int CurrentPage,int ItermNumberOfPage);

    }
}
