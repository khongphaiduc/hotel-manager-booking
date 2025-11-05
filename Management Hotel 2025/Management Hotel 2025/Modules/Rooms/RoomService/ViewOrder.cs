using Management_Hotel_2025.ViewModel;
using Microsoft.EntityFrameworkCore;
using Mydata.Models;
using System.Collections.Immutable;

namespace Management_Hotel_2025.Modules.Rooms.RoomService
{
    public class ViewOrder : IOrder
    {
        private readonly ManagermentHotelContext _dbcontext;

        public ViewOrder(ManagermentHotelContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        async Task<Order> IOrder.ViewOrder(string bookingcode)
        {
            var booking = await _dbcontext.Bookings
                .Include(s => s.BookingDetails)
                .ThenInclude(s => s.Room)
                .ThenInclude(s => s.RoomType)
                .Include(s => s.BookingDetails)
                .ThenInclude(s => s.BookingServices)
                .FirstOrDefaultAsync(o => o.BookingCode == bookingcode);

            if (booking == null)
                return null;

            var order = new Order
            {
                TimeDeposit = booking.BookingDate,
                CustomerName = booking.CustomerName,
                CustomerPhone = booking.CustomerPhone,
                PersonalId = booking.PersonalCode,
                Email = booking.Email,
                CustomerAddress = booking.Address,
                OrderDate = DateTime.Now,
                BookingCode = booking.BookingCode,
                RealCheckInDate = booking.RealTimeCheckIn ?? DateTime.MinValue, // ✅ Null coalescing
                RealCheckOutDate = booking.RealTimeCheckOut ?? DateTime.MinValue,
                Deposit = booking.DepositAmount,
                OrderStatus = "Pending",
                roomsOrders = booking.BookingDetails.Select(ro => new RoomOrder
                {
                    RoomType = ro.Room.RoomType.Name,
                    RoomNumber = ro.Room.RoomNumber,
                    PricePerNight = ro.Room.RoomType.Price,
                    NumberOfNights = ro.CheckOutDate.HasValue && ro.CheckInDate.HasValue
    ? (ro.CheckOutDate.Value - ro.CheckInDate.Value).Days
    : 0,
                    UsedToServices = ro.BookingServices.Select(bs => new BookingService
                    {
                        UnitPrice = bs.UnitPrice,
                        Quantity = bs.Quantity
                    }).ToList()
                }).ToList()
            };

            return order;
        }
    }
}
