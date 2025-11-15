using API_BookingHotel.Modules.Invoice.InvoiceModels;
using Microsoft.EntityFrameworkCore;
using Mydata.Models;

namespace API_BookingHotel.Modules.Invoice.MInvoiceServices
{
    public class InvoiceService : IInvoiceServices
    {
        private readonly ManagermentHotelContext _dbcontext;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(ManagermentHotelContext dbcontext, ILogger<InvoiceService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        //lấy hóa đơn theo mã
        public async Task<InvoiceViewModel> GetInvoicePassengerByCode(string invoiceCode)
        {
            try
            {
                var InvoicesItem = await _dbcontext.Orders.Where(s => s.OrderCode == invoiceCode)
                    .Include(s => s.Booking)
                    .ThenInclude(s => s.BookingDetails)
                    .ThenInclude(s => s.Room)
                    .Select(s => new InvoiceViewModel()
                    {
                        InvoiceCode = s.OrderId,
                        CustomerName = s.Booking.CustomerName,
                        RoomNumber = string.Join(", ", s.Booking.BookingDetails.Select(b => b.Room.RoomNumber)),
                        CheckInDate = s.Booking.RealTimeCheckIn,
                        CheckOutDate = s.Booking.RealTimeCheckOut,
                        TotalAmount = s.TotalAmount,
                        StatusInvoice = s.OrderStatus,
                        CreatedBy = "Phạm Trung Đức"
                    }).FirstOrDefaultAsync();


                return InvoicesItem ?? new InvoiceViewModel();

            }
            catch (Exception s)
            {
                _logger.LogInformation("Lỗi lấy danh sách hóa đơn: " + s.Message);
                return new InvoiceViewModel();
            }
        }


        // lấy danh sách toàn bộ hóa đơn
        public async Task<List<InvoiceViewModel>> GetListInvoicePasseners()
        {
            try
            {
                var ListInvoices = await _dbcontext.Orders
                    .Include(s => s.Booking)
                    .ThenInclude(s => s.BookingDetails)
                    .ThenInclude(s => s.Room)
                    .Select(s => new InvoiceViewModel()
                    {
                        InvoiceCode = s.OrderId,
                        CustomerName = s.Booking.CustomerName,
                        RoomNumber = string.Join(", ", s.Booking.BookingDetails.Select(b => b.Room.RoomNumber)),
                        CheckInDate = s.Booking.RealTimeCheckIn,
                        CheckOutDate = s.Booking.RealTimeCheckOut,
                        TotalAmount = s.TotalAmount,
                        StatusInvoice = s.OrderStatus,
                        CreatedBy = "Phạm Trung Đức"
                    }).ToListAsync();

                if (ListInvoices == null || ListInvoices.Count == 0)
                {
                    return new List<InvoiceViewModel>();
                }
                else
                {
                    return ListInvoices;
                }

            }
            catch (Exception s)
            {
                _logger.LogInformation("Lỗi lấy danh sách hóa đơn: " + s.Message);
                return new List<InvoiceViewModel>();
            }

        }
    }
}
