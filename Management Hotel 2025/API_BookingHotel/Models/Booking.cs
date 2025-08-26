using System;
using System.Collections.Generic;

namespace API_BookingHotel.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public DateTime BookingDate { get; set; }

    public string BookingSource { get; set; } = null!;

    public string Status { get; set; } = null!;

    public decimal DepositAmount { get; set; }

    public decimal TotalAmountBooking { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User? User { get; set; }
}
