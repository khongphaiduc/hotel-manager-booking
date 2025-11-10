using API_BookingHotel.ViewModels;

namespace API_BookingHotel.Modules.Rooms.RoomsService
{
    public interface IRoomService
    {

        public Task<List<ViewRoom>> SearchRoomByAdvance(int CurrentPage, int ItermNumberOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person, string StartDate, string EndDate,string apihost);
        public Task<List<ViewRoom>> SearchRoomByAdvanceForManagement(string option,int CurrentPage, int ItermNumberOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person, string StartDate, string EndDate);
       
      

    }
}





