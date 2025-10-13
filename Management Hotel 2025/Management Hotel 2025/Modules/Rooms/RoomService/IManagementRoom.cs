using Management_Hotel_2025.ViewModel;
using System.Collections;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public interface IManagementRoom
    {

        public void CreateRoom();
        public void UpdateRoom();
        public void DeleteRoom();
      
        public void ViewDetailRoom();

        public Task<List<ViewRoomModel>> FilterRoom(string Option, int? Floor, DateTime startdate, DateTime enddate);

        public Task<ViewRoomModel> FilterByIdRoom(string IdRoom);

    }
}
