namespace Management_Hotel_2025.Modules.Payment.PayWithCheckOut
{
    public interface IPayCheckOut
    {
        public Task<bool> PayWithCheckOut(ViewModel.Order orderViewModel);

    }
}
