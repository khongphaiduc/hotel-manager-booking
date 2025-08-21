using Management_Hotel_2025.Models;
using Management_Hotel_2025.Serives.CallAPI;
using Management_Hotel_2025.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;

namespace Management_Hotel_2025.Controllers
{
    public class ManagementRoomController : Controller
    {
        private readonly ManagermentHotelContext _dbContext;
        private readonly IApiServices _ApiService;
        private readonly HttpClient _httpClient;

        public ManagementRoomController(ManagermentHotelContext dbcontext, IApiServices api, HttpClient httpClient)
        {
            _dbContext = dbcontext;
            _ApiService = api;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        // hiện thi danh sách phòng(advance) và phân trang
        [AllowAnonymous]
        public async Task<IActionResult> ViewListRoomVer2(int PageCurrent, int NumerItemOfPage, int? Floor, int? PriceMin, int? PriceMax, int? Person)
        {
            ViewBag.Floor = Floor;
            ViewBag.PageCurrent = PageCurrent;
            ViewBag.NumerItemOfPage = NumerItemOfPage;
            ViewBag.PriceMin = PriceMin;
            ViewBag.PriceMax = PriceMax;
            ViewBag.Person = Person;

            var  model  = await _ApiService.ViewDetaiRoomAIPAsyncVer2(PageCurrent,NumerItemOfPage,Floor,PriceMin,PriceMax,Person);
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return NotFound("No rooms found.");
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> ViewDetailRoom([FromQuery] int IdRoom)
        {
            var Room = await _ApiService.ViewDetaiRoomAIPAsync(IdRoom);

            if(Room != null)
            {
                return View(Room);
            }
            else
            {
                return NotFound("Room not found.");
            }

        }
    }
}
