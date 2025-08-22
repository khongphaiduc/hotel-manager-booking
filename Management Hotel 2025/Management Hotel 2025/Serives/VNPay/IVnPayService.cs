using Management_Hotel_2025.ViewModel.VNPay;

namespace Management_Hotel_2025.Serives.VNPay
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}
