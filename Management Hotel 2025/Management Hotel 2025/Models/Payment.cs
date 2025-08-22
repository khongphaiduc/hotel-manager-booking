using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
namespace Management_Hotel_2025.Models
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }  

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty; // Cash, Card, VnPay, MoMo...

        [MaxLength(100)]
        public string? TransactionId { get; set; } // có thể null nếu thanh toán tiền mặt

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Paid, Failed, Refunded

        public DateTime CreatedAt { get; set; } = DateTime.Now;

      
        public int BookingId { get; set; }
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; } = null!;
    }
}
