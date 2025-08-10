using Management_Hotel_2025.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management_Hotel_2025.Controllers
{
    public class LogController : Controller
    {
        private readonly ManagermentHotelContext _dbcontext;

        public LogController(ManagermentHotelContext dbcontext) 
        {
            _dbcontext = dbcontext;
        }   

        // GET: LogController
        public ActionResult Login()
        {
            return View();
        }


        public  int GetListUser()
        {
           return _dbcontext.Users.Count(); 
        }
      
    }
}
