using API_BookingHotel.Modules.Invoice.InvoiceModels;

namespace API_BookingHotel.Modules.Invoice.MInvoiceServices
{
    public interface IInvoiceServices
    {
        public Task<List<InvoiceViewModel>> GetListInvoicePasseners();

        public Task<InvoiceViewModel> GetInvoicePassengerByCode(string invoiceCode);
    }
}
