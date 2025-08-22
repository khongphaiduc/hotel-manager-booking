using API_BookingHotel.Models;
using API_BookingHotel.Serives;
using API_BookingHotel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API_BookingHotel.Controllers
{
    [Route("api/RoomTotel")]
    [ApiController]
    public class RoomSerivcesController : ControllerBase
    {
        private readonly IRoomService _IRoomService;
        private readonly ManagermentHotelContext _dbcontext;

        public RoomSerivcesController(IRoomService _RoomService, ManagermentHotelContext dbcontext)
        {
            _IRoomService = _RoomService;
            _dbcontext = dbcontext;
        }


        [AllowAnonymous]
        [HttpGet("room")]
        public async Task<IActionResult> SearchRoomAdvance(int PageCurrent, int NumerItemOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person)
        {
            // lấy db trước khi mà skip
            int TotalItems = await _dbcontext.Rooms
                         .Include(s => s.RoomType)
                         .Where(s => (!Floor.HasValue || s.Floor == Floor.Value) &&
                               (!PriceMin.HasValue || s.RoomType.Price >= PriceMin.Value) &&
                               (!PriceMax.HasValue || s.RoomType.Price <= PriceMax.Value) &&
                               (!Person.HasValue || s.RoomType.MaxGuests == Person.Value)).CountAsync();

            var list = await _IRoomService.SearchRoomByAdvance(PageCurrent, NumerItemOfPage, Floor, PriceMin, PriceMax, Person);

            return Ok(new PaginationResult<ViewRoom>(list, TotalItems, PageCurrent, NumerItemOfPage));

        }

    }
}
