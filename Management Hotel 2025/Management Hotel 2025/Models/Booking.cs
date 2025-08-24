using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;

namespace Management_Hotel_2025.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }   // có thể null nếu là khách walk-in

    public string BookingSource { get; set; } = "Website";   // Website, App, Phone, WalkIn, TravelAgency    

    public string Status { get; set; } = "Pending";   // Pending, Confirmed, Cancelled, CheckedIn, CheckedOut

    public DateTime BookingDate { get; set; } = DateTime.Now;

  
    public virtual User? User { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}