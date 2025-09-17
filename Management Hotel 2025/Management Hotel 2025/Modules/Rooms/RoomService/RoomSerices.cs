

using Management_Hotel_2025.Modules.Rooms.RoomViewModel;
using Management_Hotel_2025.Serives.CallAPI;
using Management_Hotel_2025.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mydata.Models;
using System.Reflection.Metadata.Ecma335;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public class RoomSerices : IRoomService
    {
        private readonly ManagermentHotelContext _dbcontext;
        private readonly IApiServices _ApiRoom;

        public RoomSerices(ManagermentHotelContext managermentHotelContext, IApiServices apiServices)
        {
            _dbcontext = managermentHotelContext;
            _ApiRoom = apiServices;
        }



        public bool AddServicesHotel(int IdService)
        {
            throw new NotImplementedException();
        }
         // Lấy danh sách phòng của người dùng theo userId
        public List<ViewListRoomsOfUser> GetListRoomOfUser(int userId)
        {
            var listItem = _dbcontext.Bookings
       .Include(s => s.BookingDetails)
           .ThenInclude(d => d.Room)
               .ThenInclude(r => r.RoomType)
       .Where(s => s.UserId == userId && s.Status == "Success")
       .Select(s => new ViewListRoomsOfUser()
       {
           IdRoom = s.BookingDetails.Select(b => b.Room.RoomId).FirstOrDefault(),
           NumberRoom = Convert.ToInt32(s.BookingDetails.Select(s => s.Room.RoomNumber).FirstOrDefault()),
           NameRoom = s.BookingDetails.Select(b => b.Room.RoomType.Name).FirstOrDefault(),
           StatusRoom = s.BookingDetails.Select(b => b.StatusCheckRoom).FirstOrDefault(),
           Floor = s.BookingDetails.Select(b => b.Room.Floor).FirstOrDefault(),
           PriceRoom = s.BookingDetails.Select(b => b.Room.RoomType.Price).FirstOrDefault(),
           DescriptionRoom = s.BookingDetails.Select(b => b.Room.Description).FirstOrDefault(),
           ImageRoom = s.BookingDetails.Select(b => b.Room.PathImage).FirstOrDefault(),
           DateCheckIn = s.BookingDetails.Select(b => b.CheckInDate).FirstOrDefault(),
           DateCheckout = s.BookingDetails.Select(b => b.CheckOutDate).FirstOrDefault(),

       }).ToList();


            if (listItem.IsNullOrEmpty())
            {
                return new List<ViewListRoomsOfUser>();
            }
            else
            {
                return listItem;
            }
        }

        // Xem chi tiết phòng theo IdRoom
        public async Task<ViewDetailRoom> ViewDetailOfRoom(int IdRoom)
        {
            return await _ApiRoom.ViewDetaiRoomAIPAsync(IdRoom);
        }
    }
}
