namespace Management_Hotel_2025.Modules.Games.GameModel
{
    public class PaymentRequestModel
    {
        public int Amount { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }

    }
}
