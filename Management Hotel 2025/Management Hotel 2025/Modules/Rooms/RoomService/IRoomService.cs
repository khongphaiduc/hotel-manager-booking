using Management_Hotel_2025.Modules.Rooms.RoomViewModel;
using Management_Hotel_2025.ViewModel;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public interface IRoomService
    {
     
        public List<ViewListRoomsOfUser> GetListRoomOfUser(int userId);     // Lấy danh sách phòng của người dùng theo userId


        public Task<ViewDetailRoom> ViewDetailOfRoom(int IdRoom);   // Xem chi tiết phòng theo IdRoom


        public bool AddServicesHotel(int IdService);    // Thêm dịch vụ vào phòng theo IdService


    }
}
