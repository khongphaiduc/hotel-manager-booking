using Management_Hotel_2025.ViewModel;
using Microsoft.EntityFrameworkCore;
using Mydata.Models;
using System.Threading.Tasks;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public class FilterRooms : IManagementRoom
    {
        private readonly ManagermentHotelContext _Dbcontext;

        public FilterRooms(ManagermentHotelContext dbcontext)
        {

            _Dbcontext = dbcontext;
        }

        public void CreateRoom()
        {
            throw new NotImplementedException();
        }

        public void DeleteRoom()
        {
            throw new NotImplementedException();
        }

        //tìm kiếm  phòng theo số ph
        public async Task<ViewRoomModel> FilterByIdRoom(string IdRoom)
        {
            return await _Dbcontext.Rooms.Include(s => s.RoomType).Where(s => s.RoomNumber.Equals(IdRoom)).Select(s => new ViewRoomModel()
            {
                IdRoom = s.RoomId,
                Name = s.RoomType.Name,
                Floor = (int)s.Floor,
                Description = s.Description,
                Image = s.PathImage,
                Price = s.RoomType.Price,
                NumberOfRooms = s.RoomNumber
            }).FirstOrDefaultAsync() ?? new ViewRoomModel();

        }


        // lọc phòng
        public async Task<List<ViewRoomModel>> FilterRoom(string Option, int? Floor, DateTime startdate, DateTime enddate)
        {
            var list = await _Dbcontext.Rooms
                .Include(s => s.RoomType)
                .Include(s => s.BookingDetails)
                    .ThenInclude(s => s.Booking)
                .Where(s =>
                    (!Floor.HasValue || s.Floor == Floor) &&
                    (
                        Option == "all" ||
                        (Option == "Booked" && s.BookingDetails.Any(bd => bd.Booking.Status != "Cancelled" && startdate < bd.CheckOutDate && enddate > bd.CheckInDate)) ||
                        (Option == "Available" && !s.BookingDetails.Any(bd => bd.Booking.Status != "Cancelled" && startdate < bd.CheckOutDate && enddate > bd.CheckInDate)) ||
                        (Option == "Maintenance" && s.Status.Equals("Maintenance")) ||
                        (Option == "Cleaning" && s.Status.Equals("Cleaning"))
                    )
                )
                .Select(room => new ViewRoomModel()
                {
                    IdRoom = room.RoomId,
                    Name = room.RoomType.Name,
                    Floor = (int)room.Floor,
                    Description = room.Description,
                    Image = room.PathImage,
                    Price = room.RoomType.Price,
                    NumberOfRooms = room.RoomNumber,

                    // 👇 Lấy tên người booking (nếu có)
                    NamePasssger = room.BookingDetails
                        .Where(bd => bd.Booking.Status != "Cancelled" &&
                                     startdate < bd.CheckOutDate &&
                                     enddate > bd.CheckInDate)
                        .Select(bd => bd.Booking.CustomerName)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return list;
        }

        public void UpdateRoom()
        {
            throw new NotImplementedException();
        }

        public void ViewDetailRoom()
        {
            throw new NotImplementedException();
        }


        // lấy lịch của 1 phòng
        public List<BookingInfo> GetListDateBookingOfRoom(int IdRoom)
        {

            var list = _Dbcontext.BookingDetails
                 .Include(s => s.Booking)
                 .Where(s => s.RoomId == IdRoom && s.Booking.Status != "Cancelled")
                 .Select(s => new BookingInfo()
                 {
                     StartDate = s.CheckInDate.Value,
                     EndDate = s.CheckOutDate.Value,
                     CustomerName = s.Booking.CustomerName,
                     Status = s.Booking.Status,
                    
                 }).ToList();
            return list;
        }

        public void ViewDetailRoom(int idRoom)
        {
            throw new NotImplementedException();
        }
    }
}
