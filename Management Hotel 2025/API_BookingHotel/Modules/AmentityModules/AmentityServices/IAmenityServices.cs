using API_BookingHotel.ViewModels;
using MyData.Models;

namespace API_BookingHotel.Modules.AmentityModules.AmentityServices
{
    public interface IAmenityServices
    {
        public Task<List<AmentityUpdate>> GetAllAmenityAsync(string apihost);  // lấy danh sách tất cả tiện ích

        public Task<AmentityUpdate> GetAmenityByIdAsync(int id, string apihost); // lấy tiện ích theo id


        public Task<bool> CreateAmenityAsync(AmentityUpdate request); // tạo mới tiện ích


        public Task<bool> UpdateAmenityAsync(AmentityUpdate request); // cập nhật tiện ích theo id


        public Task<bool> DeleteAmenityAsync(int id); // xóa tiện ích theo id

        public Task<bool> ChangeStatusAmenityAsync(int id); // thay đổi trạng thái tiện ích theo id

    }
}
