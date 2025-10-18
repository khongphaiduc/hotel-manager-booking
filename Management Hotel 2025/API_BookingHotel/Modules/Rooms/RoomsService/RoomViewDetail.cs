
using API_BookingHotel.ViewModels;
using Microsoft.EntityFrameworkCore;
using Mydata.Models;


namespace API_BookingHotel.Modules.Rooms.RoomsService
{
    public class RoomViewDetail
    {
        private readonly ManagermentHotelContext _dbcontext;

        public RoomViewDetail(ManagermentHotelContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Allow user to view detail the room   
        public async Task<ViewRoomDetail> ViewDetailRoomAsync(int roomID)
        {

            var s = await _dbcontext.Rooms
                .Include(s => s.RoomType)
                .Include(s => s.RoomAmenities)
                .Include(s => s.Images)
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
                    ListPathImage = room.Images.Select(s => s.LinkImage).ToList(),
                    ListAmenites = room.RoomAmenities.Select(s => new Amenity()
                    {
                        AmenityId = s.Amenity.AmenityId,
                        Name = s.Amenity.Name,
                        Description = s.Amenity.Description,
                        UrlImage = s.Amenity.UrlImage

                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return s ?? new ViewRoomDetail();

        }


    }
}
