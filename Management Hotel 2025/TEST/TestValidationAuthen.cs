using Management_Hotel_2025.Serives.AuthenSerive;

namespace TEST
{
    public class TestValidationAuthen
    {
        [Theory]
        [InlineData("123456789PhamTrungDuc", true)]
        [InlineData("123456789phamtrungduc", false)]
        [InlineData("12345", false)]
        [InlineData("", false)]
        [InlineData("654321Duongbinhnhatminh", true)]
        public void Test1(string input, bool expect)
        {
            ValidationAuthen validationAuthen = new ValidationAuthen();

            var reuslt = validationAuthen.ValidatePassword(input);

            Assert.Equal(expect, reuslt);
        }



        //[Theory]
        //[InlineData("ptrungduc@gmal.com", "123456789PhamTrungDuc", true)]
        //[InlineData("ptrungduc1011@gmail.com", "123456789PhamTrungDuc", true)]
      
        //public void TestLogin(string email ,string password, bool expect)
        //{
        //    var s = new Login();

        //    var result = s.MyLogin(email, password);

        //    Assert.Equal(expect, result);
        //}
    }
}