using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;

namespace Mydata.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }   // có thể null nếu là khách walk-in

    public string BookingSource { get; set; } = "Website";   // Website, App, Phone, WalkIn, TravelAgency    

    public string Status { get; set; } = "Pending";   // Pending, Confirmed, Cancelled, CheckedIn, CheckedOut

    public DateTime BookingDate { get; set; } = DateTime.Now;

    public decimal DepositAmount { get; set; } = 0;  // số tiền mà khách đã thành toán khi đặt cọc phòng 

    public decimal TotalAmountBooking { get; set; }  // tổng số tiền của booking tiền phòng chưa bao gom dịch vụ

    public virtual User? User { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}