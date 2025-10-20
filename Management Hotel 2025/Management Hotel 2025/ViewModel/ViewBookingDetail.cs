using Mydata.Models;

namespace Management_Hotel_2025.ViewModel
{
    public class ViewBookingDetail
    {
        public string BookingId { get; set; }

        public string BookingCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BookingStatus { get; set; } // Chưa check-in, Đã check-in, Hoàn tất, Hủy
        public string BookingSource { get; set; } // Online, Walk-in, OTA


        // 2️⃣ Thông tin khách hàng
        public string CustomerFullName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerGender { get; set; }
        public string CustomerNationality { get; set; }
        public string CustomerIdentityNumber { get; set; } // CMND / Passport
        public string CustomerSpecialRequest { get; set; }

        // 3️⃣ Thông tin phòng
        public int NumberOfRoom { get; set; }

        public List<Room> Rooms { get; set; } = new List<Room>();

        public List<RoomType> RoomsType { get; set; } = new List<RoomType>();


        public List<decimal> RoomPricesPerNight { get; set; } = new List<decimal>();
        public List<string> RoomStatuses { get; set; } = new List<string>(); // Trống, Đang ở

        // 4️⃣ Thời gian lưu trú
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int NumberOfNights
        {
            get
            {
                return (CheckOutDate - CheckInDate).Days;
            }
        }

        // 5️⃣ Thông tin thanh toán


        public decimal TotalAmountRoom { get; set; }
        public decimal DepositAmount { get; set; }

        public decimal Discount { get; set; }

        public string PaymentMethod { get; set; } // Cash, Card, Transfer
      
 

        // 7️⃣ Lịch sử thay đổi & ghi chú
        public List<string> ModifiedBy { get; set; } = new List<string>();

        // 8️⃣ Thông tin khác (tuỳ chọn)
        public string QRCode { get; set; }

    }
}