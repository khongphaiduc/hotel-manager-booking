using API_BookingHotel.Models;
using API_BookingHotel.Repository;
using API_BookingHotel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_BookingHotel.Controllers
{
    [Route("api/getbooking")]
    [ApiController]
    public class BookingHotelController : ControllerBase
    {
        private readonly ManagermentHotelContext _dbcontext;
        private readonly ManagementBooking _Mybooking;

        public BookingHotelController(ManagermentHotelContext dbcontext, ManagementBooking MyBooings)
        {
            _dbcontext = dbcontext;
            _Mybooking = MyBooings;
        }



        [HttpGet("pathVersion1")]

        public async Task<List<ViewRoom>> GetVersion1()
        {
            return await _Mybooking.GetBookingsAsync();
        }


        [Authorize]
        [HttpGet("ListRoom")]
        public async Task<IActionResult> GetVersion2()
        {
            var ListRoom = await _Mybooking.GetBookingsAsync();

            if (ListRoom == null)
            {
                return Ok("Room is Empty");
            }
            else
            {
                return Ok(ListRoom);
            }

        }

    }
}
