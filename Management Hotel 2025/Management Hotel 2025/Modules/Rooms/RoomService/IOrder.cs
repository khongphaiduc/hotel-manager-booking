using Management_Hotel_2025.ViewModel;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public interface IOrder
    {

        public Task<Order> ViewOrder(string bookingcode);

    }
}
