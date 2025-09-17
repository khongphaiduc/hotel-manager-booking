
using Management_Hotel_2025.Modules.AuthenSerive;
using Mydata.Models;
using System.Security.Cryptography;
using System.Text;

namespace Management_Hotel_2025.Serives.AuthenSerive
{
    public class RegisterAccount
    {
        private readonly ManagermentHotelContext _dbcontext;
        private readonly IEncoding _Iendcoding;

        public RegisterAccount(ManagermentHotelContext dbcontext, IEncoding iencoding)
        {
            _dbcontext = dbcontext;
            _Iendcoding = iencoding;
        }

        public bool Register(string username, string phone, string email, string password)
        {
            try
            {
                if (EmailExists(email))
                {
                    return false;
                }
                else
                {
                    string satl = _Iendcoding.GenerateSalt();

                    var user = new User
                    {
                        Username = username,
                        FullName = username,
                        PhoneNumber = phone,
                        Email = email,
                        PasswordHash = _Iendcoding.HashPassword(password, satl),
                        Salt = satl,
                        Role = "User",
                        CreatedAt = DateTime.Now
                    };
                    _dbcontext.Users.Add(user);
                    _dbcontext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool EmailExists(string email)
        {
            return _dbcontext.Users.Any(u => u.Email == email);
        }


    }
}
