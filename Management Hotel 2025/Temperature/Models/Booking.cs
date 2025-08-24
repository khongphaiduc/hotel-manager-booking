using System;
using System.Collections.Generic;

namespace Temperature.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
