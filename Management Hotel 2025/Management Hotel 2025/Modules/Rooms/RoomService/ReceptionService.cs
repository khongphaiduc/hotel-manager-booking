using Microsoft.EntityFrameworkCore;
using Mydata.Models;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public class ReceptionService : IReceptionService
    {
        private readonly ManagermentHotelContext _dbcontext;

        public ReceptionService(ManagermentHotelContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Booking CheckIn(string BookingCode)
        {
            var booking = _dbcontext.Bookings.Include(s => s.BookingDetails)
                .Where(s => s.BookingCode.EndsWith(BookingCode))
                .Select(s => new Booking()
                {
                    BookingCode = s.BookingCode,
                    PersonalCode = s.PersonalCode,
                    Nationality = s.Nationality,
                    BirthDay = s.BirthDay,
                    CustomerPhone = s.CustomerPhone,
                    Email = s.Email,
                    Address = s.Address,
                    TypePassenger = s.TypePassenger,
                    BookingDetails = s.BookingDetails,
                    Status = s.Status
                }).FirstOrDefault();

            return booking ?? new Booking() { BookingCode = "0" };  // bằng 0 thì lỗi cmnr
        }

        public Booking Checkout(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
