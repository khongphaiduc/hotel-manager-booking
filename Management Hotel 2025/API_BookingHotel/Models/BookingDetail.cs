using System;
using System.Collections.Generic;

namespace API_BookingHotel.Models;

public partial class BookingDetail
{
    public int BookingDetailId { get; set; }

    public int BookingId { get; set; }

    public int RoomId { get; set; }

    public int NumberOfGuests { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();

    public virtual Room Room { get; set; } = null!;
}
