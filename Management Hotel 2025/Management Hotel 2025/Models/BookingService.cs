using System.ComponentModel.DataAnnotations.Schema;

namespace Management_Hotel_2025.Models
{
    public class BookingService
    {
        public int BookingServiceId { get; set; }

        public int BookingDetailId { get; set; }   // năm  phòng nào 

        public int ServiceId { get; set; }   // những dịch vụ sử dụng 

        public decimal UnitPrice { get; set; }  // giá dịch vụ tại thời điểm dartwd sau khi mà trừ discount

        public int Quantity { get; set; }  // số lượng dịch vụ sử dụng
        public decimal TotalPrice { get; set; }  // giá dịch vụ

        public DateTime LastUpdate { get; set; } = DateTime.Now;
        [ForeignKey("BookingDetailId")]

        public virtual BookingDetail BookingDetail { get; set; } = null!;
        [ForeignKey("ServiceId")]

        public virtual Services Service { get; set; } = null!;


    }
}
