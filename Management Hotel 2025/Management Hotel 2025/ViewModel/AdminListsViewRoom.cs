
namespace Management_Hotel_2025.ViewModel
{
    public class AdminListsViewRoom
    {

        public ICollection<RoomTemporary> ListCheckInToday { get; set; } = new List<RoomTemporary>();    // danh sách phòng khách nhận phòng hôm nay

        public ICollection<RoomTemporary> ListCheckOutToday { get; set; } = new List<RoomTemporary>(); //   danh sách phòng khách trả phòng hôm nay


        public ICollection<RoomTemporary> ListCustomerUsing { get; set; } = new List<RoomTemporary>();          // danh sách khách hàng đang lưu trú 

    }

    public class RoomTemporary
    {

        public string NameCustomer { get; set; } = "Không xác định ";

        public string TypeRoom { get; set; } = "Normal";

        public int NumberOfRoom { get; set; }

        public DateTime DayCheckIn { get; set; }

        public DateTime DayCheckOut { get; set; }

        public string TypeCustomer { get; set; } = "Sigle";


    }
}
