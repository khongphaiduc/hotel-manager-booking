using Management_Hotel_2025.Models;
using Management_Hotel_2025.Serives.CallAPI;
using Management_Hotel_2025.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Management_Hotel_2025.Controllers
{
    public class ManagementRoomController : Controller
    {
        private readonly ManagermentHotelContext _dbContext;
        private readonly IApiServices _ApiService;

        public ManagementRoomController(ManagermentHotelContext dbcontext, IApiServices api)
        {
            _dbContext = dbcontext;
            _ApiService = api;
        }




        //[Authorize(Roles = "Admin,User,staff")]
        //public IActionResult ViewListRoom()
        //{

        //    var ListRoom = _dbContext.Rooms.Include(s => s.RoomType).Select(t => new ViewRoomModel()
        //    {
        //        Name = t.RoomType.Name,
        //        Description = t.Description,
        //        Image = t.PathImage,
        //        Price = t.RoomType.Price.ToString(),
        //    }).ToList();


        //    HttpContext.Response.Headers.Append("Phamtrungduc", "DEptria");
        //    return View(ListRoom);
        //}


        public async Task<IActionResult> ViewListRoom()
        {
            var TokenTemperary = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDQ1Iiwic3ViIjoiMTIzNDU2Nzg5MCIsIm5hbWUiOiJKb2huIERvZSIsImFkbWluIjp0cnVlLCJpYXQiOjE1MTYyMzkwMjJ9.l903ZZnlAE9MB_WmC4YS27U7mC3EnAlhOti6wNbWz6Q";



            var model = await _ApiService.GetListRoomFromAPIAsync(TokenTemperary);

            if (model != null)
            {
                return View(model);
            }
            else
            {
                return NotFound("No rooms found.");
            }

        }


        public IActionResult ViewDetailRoom()
        {
            return View();
        }
    }
}
