using Management_Hotel_2025.Models;
using Management_Hotel_2025.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Management_Hotel_2025.Controllers
{
    public class ManagementRoomController : Controller
    {
        private readonly ManagermentHotelContext _dbContext;

        public ManagementRoomController(ManagermentHotelContext dbcontext)
        {
            _dbContext = dbcontext;
        }




        //[Authorize(Roles = "Admin,User,staff")]
        public IActionResult ViewListRoom()
        {

            var ListRoom = _dbContext.Rooms.Include(s => s.RoomType).Select(t => new ViewRoomModel()
            {
                Name = t.RoomType.Name,
                Description = t.Description,
                Image = t.PathImage,
                Price = t.RoomType.Price.ToString(),
            }).ToList();


            HttpContext.Response.Headers.Append("Phamtrungduc", "DEptria");
            return View(ListRoom);
        }

        public IActionResult ViewDetailRoom()
        {
            return View();
        }
    }
}
