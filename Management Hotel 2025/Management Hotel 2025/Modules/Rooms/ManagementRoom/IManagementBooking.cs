using Management_Hotel_2025.ViewModel;

namespace Management_Hotel_2025.Modules.Rooms.ManagementRoom
{
     // quản lý các thứ liên quan đến booking 
    public interface IManagementBooking
    {

        // lấy danh sách booking    
        public List<BookingItem> GetListBooking(DateTime DateStart, DateTime EndDate);
        public List<BookingItem> GetListBooking(string search);
        public List<ViewBookingDetail> ViewDetailBooking(string BookingCode);
    }

   
}
