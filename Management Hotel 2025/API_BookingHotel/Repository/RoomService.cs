using API_BookingHotel.Models;
using API_BookingHotel.Serives;
using API_BookingHotel.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_BookingHotel.Repository
{
    public class RoomService : IRoomService
    {
        private readonly ManagermentHotelContext _dbcontext;

        public RoomService(ManagermentHotelContext Dbcontext)
        {
            _dbcontext = Dbcontext;
        }


        // tìm kiếm advance của room
        public async Task<List<ViewRoom>> SearchRoomByAdvance(int CurrentPage, int ItermNumberOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person, string? StartDate, string? EndDate)
        {

            var ItemSkip = (CurrentPage - 1) * ItermNumberOfPage; // số lượng item sẽ bỏ qua

            DateTime newCheckIn = DateTime.Parse(StartDate);
            DateTime newCheckOut = DateTime.Parse(EndDate);

            var ListItem = await _dbcontext.Rooms
                          .Include(s => s.RoomType)
                          .Where(s => (!Floor.HasValue || s.Floor == Floor.Value) &&
                                (!PriceMin.HasValue || s.RoomType.Price >= PriceMin.Value) &&
                                (!PriceMax.HasValue || s.RoomType.Price <= PriceMax.Value) &&
                                (!Person.HasValue || s.RoomType.MaxGuests == Person.Value) &&
                                 !s.BookingDetails.Any(bd =>
                                                       bd.Booking.Status != "Cancelled" &&
                                                       newCheckIn < bd.CheckOutDate &&
                                                       newCheckOut > bd.CheckInDate )
                                      
                                )
                          .OrderBy(s => s.RoomType.Price)
                          .Skip(ItemSkip)
                          .Take(ItermNumberOfPage)
                          .Select(room => new ViewRoom()
                          {
                              IdRoom = room.RoomId,
                              Name = room.RoomType.Name,
                              Floor = (int)room.Floor,
                              Description = room.Description,
                              Image = room.PathImage,
                              Price = room.RoomType.Price
                          })
                             .ToListAsync();

            return ListItem;
        }


    }
}
