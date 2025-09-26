using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Mydata.Models;

namespace Management_Hotel_2025.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ManagermentHotelContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, ManagermentHotelContext context)
        {
            _logger = logger;
            _dbcontext = context;
        }

        public IActionResult Index()
        {
            // nếu là nhân viên hay là thằng admin thì chuyển giao diện
            if (User.IsInRole("Staff") || User.IsInRole("Admin"))
            {
                return RedirectToAction("StaffViewListRoom", "StaffManagementRoom");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
