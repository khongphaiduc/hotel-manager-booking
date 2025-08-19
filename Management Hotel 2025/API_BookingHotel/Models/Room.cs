using System;
using System.Collections.Generic;

namespace API_BookingHotel.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int RoomTypeId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public int? Floor { get; set; }

    public string? Status { get; set; }

    public string Description { get; set; } = null!;

    public string PathImage { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual RoomType RoomType { get; set; } = null!;
}
