using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Mydata.Models
{
    [Table("Guest")]
    public class Guests
    {
        [Key]

        public int GuestId { get; set; }

        [Required]
        public string CodePersonal { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = null!;


        public string? Gender { get; set; }


        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Nationality { get; set; } = null!;

        public string Note { get; set; }

        public DateTime? TimeCheckIn { get; set; }

        public DateTime? TimeCheckOut { get; set; }

        public int BookingDetailId { get; set; }

        [ForeignKey("BookingDetailId")]
        public virtual BookingDetail BookingDetail { get; set; } = null!;


    }
}
