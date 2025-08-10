namespace Management_Hotel_2025.Serives.Interface
{
    public interface IEncoding
    {

        public string GenerateSalt();

        public string HashPassword(string password, string salt);

    }
}
