using Management_Hotel_2025.Modules.AdminMPassengers.MPassengerModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Mydata.Models;

namespace Management_Hotel_2025.Modules.AdminMPassengers.MPassengersServices
{
    public class AdminMPassengers : IAdminMPassengers
    {
        private readonly ManagermentHotelContext _dbcontext;

        public AdminMPassengers(ManagermentHotelContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<PassengersInfo>> GetListViewPassengers()
        {
            var ListPassengers = await _dbcontext.Orders
        // Nếu Orders có mối quan hệ 1-1 hoặc 1-n với Booking, bạn có thể cần Include.
        // Nếu bạn muốn lọc, hãy dùng Where, không dùng Include như trong code gốc.
        .Where(o => o.OrderStatus == "Completed") // Ví dụ: Lọc đơn đã hoàn thành
        .GroupBy(o => new
        {
            // Nhóm theo PersonalCode VÀ các trường thông tin cá nhân 
            // để đảm bảo tính nhất quán.
            o.Booking.PersonalCode,
            o.Booking.CustomerName,
            o.Booking.Email,
            o.Booking.CustomerPhone,
            o.Booking.Address,
            o.Booking.Nationality

        })
        .Select(g => new PassengersInfo()
        {
            PersonalCode = g.Key.PersonalCode,
            Name = g.Key.CustomerName,
            Email = g.Key.Email,
            PhoneNumber = g.Key.CustomerPhone,
            Address = g.Key.Address,
            Nationality = g.Key.Nationality,
            TimesVisited = g.Count()
        })
        .ToListAsync();

            return ListPassengers ?? new List<PassengersInfo>();
        }
    }
}
