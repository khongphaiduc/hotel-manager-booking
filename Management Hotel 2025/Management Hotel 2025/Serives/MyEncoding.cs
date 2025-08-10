using Management_Hotel_2025.Serives.Interface;
using System.Security.Cryptography;
using System.Text;

namespace Management_Hotel_2025.Serives
{
    public class MyEncoding : IEncoding
    {
        public static object UTF8 { get; internal set; }

        public string GenerateSalt()
        {
            // Generate a random salt for password hashing
            //byte[] salt = new byte[16];
            //using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(salt);
            //}
            //return Convert.ToBase64String(salt);

            char[] satl = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

            Random random = new Random();
            StringBuilder saltBuilder = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                saltBuilder.Append(satl[random.Next(0, satl.Length)]);
            }
            return saltBuilder.ToString();
        }

        public string HashPassword(string password, string salt)
        {
            // byte chỉ là một nhóm gồm 8 bit (mỗi bit là 0 hoặc 1).
            byte[] PasswordByte = Encoding.UTF8.GetBytes(password);
            byte[] SaltByte = Encoding.UTF8.GetBytes(salt);


            using (var pbkdf2 = new Rfc2898DeriveBytes(PasswordByte, SaltByte, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 256-bit   nếu là 32 thì có nghĩa là lấy 32 số đầu tiên sau khi mã hoa
                return Convert.ToBase64String(hash);
            }
        }
    }
}
