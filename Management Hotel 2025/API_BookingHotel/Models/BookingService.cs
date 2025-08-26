using System;
using System.Collections.Generic;

namespace API_BookingHotel.Models;

public partial class BookingService
{
    public int BookingServiceId { get; set; }

    public int BookingDetailId { get; set; }

    public int ServiceId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual BookingDetail BookingDetail { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
