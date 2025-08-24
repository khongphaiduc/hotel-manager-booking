using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Management_Hotel_2025.Models
{
    [Table("BookingDetails")]
    public class BookingDetail
    {
        [Key]
        public int BookingDetailId { get; set; }

        public int BookingId { get; set; }

        public int RoomId { get; set; }

        public int NumberOfGuests { get; set; }

        public DateTime? CheckInDate { get; set; }

        public DateTime? CheckOutDate { get; set; }


        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;


        [ForeignKey("BookingId")]

        public virtual Booking Booking { get; set; } = null!;
        public virtual ICollection<Guests> Guests { get; set; } = new List<Guests>();

        public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }

}
