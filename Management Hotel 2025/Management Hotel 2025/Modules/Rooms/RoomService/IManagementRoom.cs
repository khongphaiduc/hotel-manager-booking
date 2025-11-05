using Management_Hotel_2025.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public interface IManagementRoom
    {

        public void CreateRoom();
        public void UpdateRoom();
        public void DeleteRoom();
      
        public void ViewDetailRoom(int idRoom);

        // lọc phòng
        public Task<List<ViewRoomModel>> FilterRoom(string Option, int? Floor, DateTime startdate, DateTime enddate);

        // tìm kiếm phòng theo số phòng
        public Task<ViewRoomModel> FilterByIdRoom(string IdRoom);

        // lấy lịch  booking của phòng
        public List<BookingInfo> GetListDateBookingOfRoom(int IdRoom);


        // lấy trạng thái phòng trong ngày
        public List<MapRoom> getListMapRoomToDay();


        public RoomPassengers ViewDetailRoomPassengers(int idbookingdetail);
    }
}
