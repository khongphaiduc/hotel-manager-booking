using Management_Hotel_2025.Models;
using Management_Hotel_2025.Serives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Controllers
{
    public class LogController : Controller
    {
        private readonly ManagermentHotelContext _dbContext;
        private readonly RegisterAccount _MyRegister;

        public LogController(ManagermentHotelContext dbcontext, RegisterAccount MyRegister)
        {
            _dbContext = dbcontext;
            _MyRegister = MyRegister;
        }


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

            if (!NewPassword.Equals(ConfirmPassword))
            {
                ViewBag.Error = "Password and Confirm Password do not match.";
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
                ViewBag.Error = "Đăng Ký Không Thành Công";
                return View(Users);
            }

            // đăng ký thành công chuyển đến controller đăng nhập
        }

        [HttpGet]
        public ActionResult RegisterAccount()
        {
            return View();
        }


    }
}
