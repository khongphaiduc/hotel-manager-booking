using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Management_Hotel_2025.Models
{
    [Table("Token")]
    public class MyToken
    {
        [Key]
        public int IdToken { get; set; }

        [StringLength(500)]

        public string? ToketContent { get; set; }


        public DateTime Daycre { get; set; }

        public DateTime DayExpired { get; set; }

        public int IdUser { get; set; }

        [ForeignKey("IdUser")]

        public virtual User User { get; set; } = null!;

    }
}
