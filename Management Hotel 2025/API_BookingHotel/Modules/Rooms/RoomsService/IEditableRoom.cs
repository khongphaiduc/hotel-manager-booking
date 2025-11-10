using API_BookingHotel.ViewModels;

namespace API_BookingHotel.Modules.Rooms.RoomsService
{
    public interface IEditableRoom
    {
        // chỉnh sửa phòng 
        public Task<bool> EditRoomStatus(AdJustRoom room);


        // lấy  toàn bộ thông tin của 1 phòng 
        public Task<AdJustRoom> GetFullInfoRoom(int roomId, string apihost);

        // tạo phòng mới
        public Task<bool> CreateNewRoom(AdJustRoom room);

    }
}
