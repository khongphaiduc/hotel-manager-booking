
using API_BookingHotel.Modules.Rooms.RoomsService;
using API_BookingHotel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mydata.Models;
using System.Threading.Tasks;

namespace API_BookingHotel.Modules.Rooms.RoomsController
{
    [Route("api/roomtotel")]
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
        public async Task<IActionResult> SearchRoomAdvance(int PageCurrent, int NumerItemOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person, string? StartDate, string? EndDate)
        {

            // nếu user không chọn ngày thì mặc định tính từ ngay hôm nay tói 7 ngày tiếp theo 
            if (StartDate == null)
            {
                StartDate = DateTime.Now.ToString();
            }
            if (EndDate == null)
            {
                DateTime Today = DateTime.Now;
                EndDate = Today.AddDays(7).ToString();
            }

            DateTime newCheckIn = DateTime.Parse(StartDate);
            DateTime newCheckOut = DateTime.Parse(EndDate);

            // lấy db trước khi mà skip
            int TotalItems = await _dbcontext.Rooms
                         .Include(s => s.RoomType).Include(s => s.BookingDetails)
                         .Where(s => (!Floor.HasValue || s.Floor == Floor.Value) &&
                               (!PriceMin.HasValue || s.RoomType.Price >= PriceMin.Value) &&
                               (!PriceMax.HasValue || s.RoomType.Price <= PriceMax.Value) &&
                               (!Person.HasValue || s.RoomType.MaxGuests == Person.Value) &&
                                                      !s.BookingDetails.Any(bd =>
                                                       bd.Booking.Status != "Cancelled" &&
                                                       newCheckIn < bd.CheckOutDate &&
                                                       newCheckOut > bd.CheckInDate))

                         .CountAsync();

            var ListResult = await _IRoomService.SearchRoomByAdvance(PageCurrent, NumerItemOfPage, Floor, PriceMin, PriceMax, Person, StartDate, EndDate);

            return Ok(new PaginationResult<ViewRoom>(ListResult, TotalItems, PageCurrent, NumerItemOfPage, newCheckIn, newCheckOut));  //  lưu vào construcor của PaginationResult để trả về

        }


        // lấy danh sách phòng cho thằng management
        [AllowAnonymous]
        [HttpGet("rooms")]
        public async Task<IActionResult> GetListRoomForManagement(string option, int PageCurrent, int NumerItemOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person, string? StartDate, string? EndDate)
        {
            // nếu user không chọn ngày thì mặc định tính từ ngay hôm nay tói 7 ngày tiếp theo 
            if (StartDate == null)
            {
                StartDate = DateTime.Now.ToString();
            }
            if (EndDate == null)
            {
                DateTime Today = DateTime.Now;
                EndDate = Today.AddDays(7).ToString();
            }

            DateTime newCheckIn = DateTime.Parse(StartDate);
            DateTime newCheckOut = DateTime.Parse(EndDate);

            // lấy db trước khi mà skip
            int TotalItems = await _dbcontext.Rooms
                         .Include(s => s.RoomType).Include(s => s.BookingDetails)
                         .Where(s => (!Floor.HasValue || s.Floor == Floor.Value) &&
                               (!PriceMin.HasValue || s.RoomType.Price >= PriceMin.Value) &&
                               (!PriceMax.HasValue || s.RoomType.Price <= PriceMax.Value) &&
                               (!Person.HasValue || s.RoomType.MaxGuests == Person.Value)
                                                      )

                         .CountAsync();

            var ListResult = await _IRoomService.SearchRoomByAdvanceForManagement(option,PageCurrent, NumerItemOfPage, Floor, PriceMin, PriceMax, Person, StartDate, EndDate);

            return Ok(new PaginationResult<ViewRoom>(ListResult, TotalItems, PageCurrent, NumerItemOfPage, newCheckIn, newCheckOut));  //  lưu vào construcor của PaginationResult để trả về
        }

    }
}
