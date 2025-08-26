using API_BookingHotel.Models;
using API_BookingHotel.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_BookingHotel.Repository
{
    public class ManagementBooking
    {
        private readonly ManagermentHotelContext _dbcontext;

        public ManagementBooking(ManagermentHotelContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Allow user to view detail the room   
        public async Task<ViewRoomDetail> ViewDetailRoomAsync(int roomID)
        {

            var s = await _dbcontext.Rooms
                .Include(s => s.RoomType)
                .Where(s => s.RoomId == roomID)
                .Select(room => new ViewRoomDetail
                {
                    RoomId = room.RoomId,
                    RoomTypeId = room.RoomTypeId,
                    NameType = room.RoomType.Name,
                    RoomNumber = room.RoomNumber,
                    Floor = (int)room.Floor,
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
