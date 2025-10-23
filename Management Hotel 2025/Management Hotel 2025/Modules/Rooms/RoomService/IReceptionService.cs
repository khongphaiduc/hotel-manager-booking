using Mydata.Models;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public interface IReceptionService
    {

        public Booking CheckIn(string BookingCode);

        public Booking Checkout(Booking booking);

    }
}
