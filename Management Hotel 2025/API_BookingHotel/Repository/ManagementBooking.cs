using API_BookingHotel.Models;
using API_BookingHotel.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_BookingHotel.Repository
{
    public class ManagementBooking
    {
        private readonly ManagermentHotelContext _dbcontext;

        public ManagementBooking(ManagermentHotelContext dbcontext )
        {
            _dbcontext =dbcontext;
        }

        public async Task<List<ViewRoom>> GetBookingsAsync()
        {
         
            var ListRoom = await _dbcontext.Rooms
                .Include(s=>s.RoomType)
                .Select(room => new ViewRoom
                {
                    Name = room.RoomType.Name, 
                    Description = room.Description,
                    Image = room.PathImage,
                    Price = room.RoomType.Price.ToString()
                })
                .ToListAsync();
            return ListRoom;
        }

       
    }
}
