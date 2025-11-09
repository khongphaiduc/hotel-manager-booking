using Management_Hotel_2025.Modules.WorkFile;
using Management_Hotel_2025.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Mydata.Models;
using MyData.Models;

namespace Management_Hotel_2025.Modules.Rooms.RoleAdmin.AdminServices
{
    public class AdminManagement : IAdminManagement
    {
        private readonly ManagermentHotelContext _dbcontext;
        private readonly IMyFiles _file;

        public AdminManagement(ManagermentHotelContext dbcontext, IMyFiles file)
        {
            _dbcontext = dbcontext;
            _file = file;
        }

     

        public AdminListsViewRoom ViewListRoom()
        {
            // danh sách phòng khách nhận phòng hôm nay
            var item1 = _dbcontext.BookingDetails.Include(s => s.Room).ThenInclude(s => s.RoomType).Include(s => s.Booking)
                .Where(bd => bd.CheckInDate == DateTime.Now.Date)
                .Select(bd => new RoomTemporary
                {
                    NameCustomer = bd.Booking.CustomerName,
                    TypeRoom = bd.Room.RoomType.Name,
                    NumberOfRoom = int.Parse(bd.Room.RoomNumber),
                    DayCheckIn = (DateTime)bd.CheckInDate,
                    DayCheckOut = (DateTime)bd.CheckOutDate,
                    TypeCustomer = bd.Booking.TypePassenger
                }).ToList();



            // danh sách phòng khách trả phòng hôm nay
            var item2 = _dbcontext.BookingDetails.Include(s => s.Room).ThenInclude(s => s.RoomType).Include(s => s.Booking)
                .Where(bd => bd.CheckOutDate == DateTime.Now.Date)
                .Select(bd => new RoomTemporary
                {
                    NameCustomer = bd.Booking.CustomerName,
                    TypeRoom = bd.Room.RoomType.Name,
                    NumberOfRoom = int.Parse(bd.Room.RoomNumber),
                    DayCheckIn = (DateTime)bd.CheckInDate,
                    DayCheckOut = (DateTime)bd.CheckOutDate,
                    TypeCustomer = bd.Booking.TypePassenger
                }).ToList();



            // danh sách khách hàng đang lưu trú
            var item3 = _dbcontext.BookingDetails.Include(s => s.Room).ThenInclude(s => s.RoomType).Include(s => s.Booking)
                .Where(bd => bd.CheckInDate <= DateTime.Now.Date && bd.CheckOutDate >= DateTime.Now.Date)
                .Select(bd => new RoomTemporary
                {
                    NameCustomer = bd.Booking.CustomerName,
                    TypeRoom = bd.Room.RoomType.Name,
                    NumberOfRoom = int.Parse(bd.Room.RoomNumber),
                    DayCheckIn = (DateTime)bd.CheckInDate,
                    DayCheckOut = (DateTime)bd.CheckOutDate,
                    TypeCustomer = bd.Booking.TypePassenger
                }).ToList();



            AdminListsViewRoom adminListsViewRoom = new AdminListsViewRoom()
            {
                ListCheckInToday = item1,
                ListCheckOutToday = item2,
                ListCustomerUsing = item3
            };

            return adminListsViewRoom;

        }

        // admim xem danh sách phòng
        public List<ViewRoomModel> ViewTypeRoom()
        {

            var items = _dbcontext.Rooms.Include(r => r.RoomType)
                .Select(r => new ViewRoomModel
                {
                    IdRoom = r.RoomId,
                    Name = r.RoomType.Name,
                    NumberOfRooms = r.RoomNumber,
                    Floor = r.Floor ?? 0,
                    Description = r.Description,
                    Price = r.RoomType.Price,
                    status = r.Status ?? "Hệ thông không nhận diện"
                }).ToList();
            return items;


        }
    }
}
