using Mydata.Models;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public interface IReceptionService
    {
        // hiển thị thông tin sẵn có ở Booking
        public Booking CheckIn(string BookingCode);

        //cập nhận booking  (check-in)
        public bool CheckIn(Booking booking);
        public Booking Checkout(Booking booking);

        public bool RegisterGuestInfo(Guests guests);

    }
}
