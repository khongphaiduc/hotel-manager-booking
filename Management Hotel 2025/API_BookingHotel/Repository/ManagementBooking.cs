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

        // Get list of rooms in hotel
        public async Task<List<ViewRoom>> GetListRoomHotelAsync()
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

        public async Task<ViewRoomDetail> ViewDetailRoomAsync(int roomID)
        {
            
            var s  = await _dbcontext.Rooms
                .Include(s => s.RoomType)
                .Where(s => s.RoomId == roomID)
                .Select(room => new ViewRoomDetail
                {
                    RoomId = room.RoomId,
                    RoomTypeId = room.RoomTypeId,
                    RoomNumber = room.RoomNumber,
                    Floor = room.Floor,
                    Status = room.Status,
                    Description = room.Description,
                    PathImage = room.PathImage,
                    Price = room.RoomType.Price,              
                    MaxGuests = room.RoomType.MaxGuests.ToString(),
                })
                .FirstOrDefaultAsync();

            return s ?? new ViewRoomDetail();
            
        }   


    }
}
