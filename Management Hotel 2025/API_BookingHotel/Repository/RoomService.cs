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

        public async Task<List<ViewRoom>> GetListRoomHotelAsync(int CurrentPage, int ItermNumberOfPage)
        {

            if (ItermNumberOfPage <= 0)
            {
                ItermNumberOfPage = 10;
            }

            if (CurrentPage <= 0)
            {
                CurrentPage = 1;
            }

            int skip = (CurrentPage - 1) * ItermNumberOfPage;   // số lượng item sẽ bỏ qua


            var ListItem = await _dbcontext.Rooms
                .Include(s => s.RoomType)
                .Select(room => new ViewRoom
                {
                    IdRoom = room.RoomId,
                    Name = room.RoomType.Name,
                    Description = room.Description,
                    Image = room.PathImage,
                    Price = room.RoomType.Price.ToString()
                })
                .Skip(skip)
                .Take(ItermNumberOfPage)
                .ToListAsync();

            if (ListItem == null)
            {
                return new List<ViewRoom>();
            }
            else
            {
                return ListItem;
            }
        }


      
    }
}
