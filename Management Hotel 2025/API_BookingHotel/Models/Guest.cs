using System;
using System.Collections.Generic;

namespace API_BookingHotel.Models;

public partial class Guest
{
    public int GuestId { get; set; }

    public string CodePersonal { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime BirthDay { get; set; }

    public string Address { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public DateTime? TimeCheckIn { get; set; }

    public DateTime? TimeCheckOut { get; set; }

    public int BookingDetailId { get; set; }

    public virtual BookingDetail BookingDetail { get; set; } = null!;
}
