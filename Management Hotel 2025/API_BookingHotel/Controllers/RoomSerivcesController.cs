using API_BookingHotel.Models;
using API_BookingHotel.Serives;
using API_BookingHotel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_BookingHotel.Controllers
{
    [Route("api/RoomTotel")]
    [ApiController]
    public class RoomSerivcesController : ControllerBase
    {
        private readonly IRoomService _IRoomService;
        private readonly ManagermentHotelContext _dbcontext;

        public RoomSerivcesController(IRoomService _RoomService,ManagermentHotelContext dbcontext)
        {
            _IRoomService = _RoomService;
            _dbcontext = dbcontext;
        }

        [AllowAnonymous]
        [HttpGet("SearchRoom")]
        public async Task<IActionResult> GetRoomList([FromQuery] PaginationRequest pagination)
        {
            // Validate the pagination request
            pagination.PageCurrent = pagination.ValidatePageNumber();
            pagination.NumberItemOfPage = pagination.ValidatePageSize();
           
            var list = await _IRoomService.GetListRoomHotelAsync(pagination.PageCurrent,pagination.NumberItemOfPage);

            var TotalItems = _dbcontext.Rooms.Count();

            if (list == null || !list.Any())
            {
                return NotFound("No rooms found.");
            }
            return Ok(new PaginationResult<ViewRoom>(list, TotalItems, pagination.PageCurrent, pagination.NumberItemOfPage));

        }
        
    }
}
