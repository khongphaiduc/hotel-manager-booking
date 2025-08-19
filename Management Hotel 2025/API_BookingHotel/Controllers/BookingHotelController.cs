using API_BookingHotel.Models;
using API_BookingHotel.Repository;
using API_BookingHotel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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


        [AllowAnonymous]        // api public means everyonce can accessable
        [HttpGet("ListRoom")]
        public async Task<IActionResult> GetlistRoom()
        {
            var ListRoom = await _Mybooking.GetListRoomHotelAsync();

            if (ListRoom == null)
            {
                return Ok("Room is Empty");
            }
            else
            {
                return Ok(ListRoom);      // Trong WEB API của ASP.NET thì các model chuyền qua OK(object) sẽ tự động chuyển thành JSON
            }

        }


        // Allow user to view detail the room
        [AllowAnonymous]
        [HttpGet("ViewDetailRoom/{idRoom}")]
        public async Task<IActionResult> ViewDetaiRoom([FromRoute] string idRoom)
        {
            if (string.IsNullOrEmpty(idRoom))
            {
                return BadRequest("Room ID is required");
            }
            else
            {
                var result = await _Mybooking.ViewDetailRoomAsync(int.Parse(idRoom));
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Room ID is required");
                }
            }
        }

    }
}
