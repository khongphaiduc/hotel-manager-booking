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

        public bool AdjustRoom(AdJustRoom room)
        {

            var item = _dbcontext.Rooms.FirstOrDefault(r => r.RoomId == room.RoomId);

            if (item != null)
            {
                item.RoomTypeId = room.RoomTypeId;
                item.RoomNumber = room.RoomNumber;
                item.Floor = room.Floor;
                item.Description = room.Description;
                item.PricePrivate = room.PricePerNight;


                return _dbcontext.SaveChanges() > 0;
            }


            return false;
        }

        // xem chi tiết thông tin của 1 phòng 
        public AdJustRoom GetRoomDetails(int roomId)
        {

            var allRoomTypes = _dbcontext.RoomTypes
                 .Select(rt => new RoomTypeViewModel
                 {
                     RoomTypeId = rt.RoomTypeId,
                     TypeName = rt.Name
                 }).ToList();


            var item = _dbcontext.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.RoomAmenities)
                .ThenInclude(ra => ra.Amenity)
                .Include(r => r.Images).Where(s => s.RoomId == roomId).Select(s => new AdJustRoom()
                {

                    RoomId = roomId,
                    RoomTypeId = s.RoomTypeId,
                    RoomNumber = s.RoomNumber,
                    Floor = s.Floor ?? 1,
                    PricePerNight = s.PricePrivate != 0 ? s.PricePrivate : s.RoomType.Price,  // nếu có giá giêng thì lấy khong thì lấy theo loại phòng
                    Description = s.Description,


                    AllRoomTypes = allRoomTypes,

                    CurrentAmenities = s.RoomAmenities.Select(s => new AmenityViewModel()
                    {
                        Id = s.AmenityId,
                        Name = s.Amenity.Name,


                    }).ToList(),


                    CurrentImages = s.Images.Select(s => new ImageViewModel()
                    {
                        Id = s.IdImage,
                        Url = s.LinkImage

                    }).ToList()

                }).FirstOrDefault();

            return item ?? new AdJustRoom()
            {
                RoomId = 0                              // retrun  =  0 thì có nghĩa là không tìm thấy phòng cần xem
            };

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
