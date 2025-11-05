using Management_Hotel_2025.ViewModel;
using Microsoft.EntityFrameworkCore;
using Mydata.Models;

namespace Management_Hotel_2025.Modules.Rooms.RoleAdmin.AdminServices
{
    public class AdminManagement : IAdminManagement
    {
        private readonly ManagermentHotelContext _dbcontext;

        public AdminManagement(ManagermentHotelContext dbcontext)
        {
            _dbcontext = dbcontext;
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
    }
}
