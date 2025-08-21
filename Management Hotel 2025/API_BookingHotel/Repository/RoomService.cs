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
        public async Task<List<ViewRoom>> SearchRoomByAdvance(int CurrentPage, int ItermNumberOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person)
        {

            var ItemSkip = (CurrentPage - 1) * ItermNumberOfPage; // số lượng item sẽ bỏ qua

            var ListItem = await _dbcontext.Rooms
                          .Include(s => s.RoomType)
                          .Where(s => (!Floor.HasValue || s.Floor == Floor.Value) &&
                                (!PriceMin.HasValue || s.RoomType.Price >= PriceMin.Value) &&
                                (!PriceMax.HasValue || s.RoomType.Price <= PriceMax.Value) &&
                                (!Person.HasValue || s.RoomType.MaxGuests >= Person.Value))
                          .OrderBy(s => s.RoomType.Price) 
                          .Skip(ItemSkip)                 
                          .Take(ItermNumberOfPage)
                          .Select(room => new ViewRoom()
                            {
                               IdRoom = room.RoomId,
                               Name = room.RoomType.Name,
                               Floor = room.Floor,
                               Description = room.Description,
                               Image = room.PathImage,
                               Price = room.RoomType.Price.ToString()
                              })
                             .ToListAsync(); 

            return ListItem;
        }


    }
}
