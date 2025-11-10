using Management_Hotel_2025.ViewModel;

namespace Management_Hotel_2025.Modules.Rooms.RoleAdmin.AdminServices
{
    public interface IAdminManagement
    {
        public AdminListsViewRoom ViewListRoom(); // xem danh sách phòng  trong ngày  ( đang đến , đang chuanr bị chek-out , số khách đang lưu chú )      

        public List<ViewRoomModel> ViewTypeRoom(); // xem loại phòng hiện có trong khách sạn

        public List<ViewRoomModel> SearchRoom(int? floor, string? status, string? key);


        public Task<bool> HideRoom (int idRoom); // ẩn phòng

        public AdJustRoom LoadTypeRoomAndAmentity();


        public List<int> NumberOfFloor();

        public List<string> StatusRoom();


    }
}
