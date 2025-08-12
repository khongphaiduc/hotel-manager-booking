using AspNetCoreGeneratedDocument;
using Management_Hotel_2025.Models;
using Management_Hotel_2025.Serives.AuthenSerive;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Management_Hotel_2025.Controllers
{
    public class AuthenController : Controller
    {
        private readonly ManagermentHotelContext _dbContext;
        private readonly RegisterAccount _MyRegister;
        private readonly ValidationAuthen _Validation;
        private readonly Login _Login;

        public AuthenController(ManagermentHotelContext dbcontext, RegisterAccount MyRegister, ValidationAuthen Validation, Login login)
        {
            _dbContext = dbcontext;
            _MyRegister = MyRegister;
            _Validation = Validation;
            _Login = login;
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromBody] User users)
        {
            string email = users.Email;
            string password = users.PasswordHash;

            // 1. Kiểm tra đăng nhập
            var result = _Login.MyLogin(email, password);
            if (!result)
            {
                return Json(new { success = false });
            }

            // 2. Lấy thông tin user từ DB (bao gồm role)
            var userFromDb = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (userFromDb == null)
            {
                return Json(new { success = false });
            }

            // 3. Tạo claims
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, userFromDb.Email),
               new Claim(ClaimTypes.Role, userFromDb.Role)   // Role từ DB
            };

            // 4. Tạo identity & principal
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // 5. Đăng nhập (lưu cookie)
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal);

            // 6. Trả JSON
            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegisterAccount(User Users)
        {
            string NewAccount = Users.Email;
            string NewPassword = Request.Form["Password"];
            string Phone = Users.PhoneNumber;
            string ConfirmPassword = Request.Form["ConfirmPassword"];



            if (NewPassword != null && !NewPassword.Equals(ConfirmPassword))
            {
                ViewBag.Error = "Password and Confirm Password do not match.";
                return View(Users);
            }
            else if (Users.PhoneNumber != null && _Validation.ExistPhoneNumber(Users.PhoneNumber))
            {
                ViewBag.Error = "Số điện thoại đã được sử dụng, vui lòng nhập số khác.";
                return View(Users);
            }
            else if (!_Validation.ValidateEmail(NewAccount))
            {
                ViewBag.Error = "Email không hợp lệ.";
                return View(Users);
            }
            else if (NewPassword != null && !_Validation.ValidatePassword(NewPassword))
            {
                ViewBag.Error = "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường và số.";
                return View(Users);
            }
            else if (Phone != null && !_Validation.ValidatePhoneNumber(Phone))
            {
                ViewBag.Error = "Số điện thoại không hợp lệ.";
                return View(Users);

            }


            bool Result = _MyRegister.Register(Users.Username, Users.PhoneNumber, NewAccount, NewPassword);

            if (Result)
            {

                ViewBag.Status = "Đăng ký thành công";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Error = "Email đã được sử dụng từ trước ";
                return View(Users);
            }

            // đăng ký thành công chuyển đến controller đăng nhập
        }

        [HttpGet]
        public ActionResult RegisterAccount()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Denied()
        {
            return View();
        }


    }
}
