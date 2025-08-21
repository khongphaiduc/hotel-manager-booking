using API_BookingHotel.ViewModels;

namespace API_BookingHotel.Serives
{
    public interface IRoomService
    {

        public Task<List<ViewRoom>> SearchRoomByAdvance(int CurrentPage, int ItermNumberOfPage,int? Floor , int? PriceMin, int? PriceMax , int? Person);
    }
}
