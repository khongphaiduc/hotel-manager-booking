using Management_Hotel_2025.Modules.AmentityModules.AmentityServices;
using Microsoft.AspNetCore.Mvc;
using Mydata.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Management_Hotel_2025.Modules.AmentityModules.AmentityControllers
{

    [Route("admin")]
    public class ManagementAmenityController : Controller
    {

        // lấy dánh sách các tiện ích 
        [HttpGet("amenity")]
        public async Task<IActionResult> ViewListAmentity()
        {
            string url = "https://localhost:7236/api/amenity";

            using (var httpclient = new HttpClient())
            {

                var response = await httpclient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    var amentity = JsonConvert.DeserializeObject<List<MyAmenity>>(jsonString);

                    return View(amentity);
                }
                else
                {
                    return View(new List<MyAmenity>());
                }

            }

        }

        // xóa tiện ích
        [HttpDelete("amenity/{id}")]
        public async Task<IActionResult> DeleteAmentity(int id)
        {
            string url = $"https://localhost:7236/api/amenity/{id}";

            using (var httpclient = new HttpClient())
            {

                var response = await httpclient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {


                    return Ok(new { message = "Xóa thành công" });
                }
                else
                {
                    return NotFound("Không tìm thấy Amenity");
                }

            }

        }


        // ẩn tiện ích 
        [HttpPatch("amenity/{id}")]
        public async Task<IActionResult> HideAmentity(int id)
        {
            string url = $"https://localhost:7236/api/amenity/{id}";

            using (var httpclient = new HttpClient())
            {
                var emptyContent = new StringContent("", System.Text.Encoding.UTF8, "application/json");
                var response = await httpclient.PatchAsync(url, emptyContent);

                if (response.IsSuccessStatusCode)
                {


                    return Ok(new { status = true, message = "Thành Công" });
                }
                else
                {
                    return NotFound("Không tìm thấy Amenity");
                }

            }

        }
        // hiển thị form tạo mới tiện ích
        [HttpGet("amenitys")]
        public IActionResult ViewCreateAmentity()
        {
            return View();
        }

        //tạo mới tiện ích
        [HttpPost("amenity")]
        public async Task<IActionResult> CreateAmentity(MyAmenity request)
        {
            string url = $"https://localhost:7236/api/amenity";

            using (var httpclient = new HttpClient())
            {

                try
                {
                    using (var client = new HttpClient())
                    {
                        using (var content = new MultipartFormDataContent())
                        {
                            content.Add(new StringContent(request.Name), "Name");
                            content.Add(new StringContent(request.Status ?? ""), "Status");
                            content.Add(new StringContent(request.Description ?? ""), "Description");

                            if (request.UpdateImage == null)
                            {

                                return BadRequest(new { status = false, message = "Image not found" });

                            }

                            var fileContent = new StreamContent(request.UpdateImage.OpenReadStream());
                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.UpdateImage.ContentType);
                            content.Add(fileContent, "UpdateImage", request.UpdateImage.FileName);

                            HttpResponseMessage response = await client.PostAsync(url, content);

                            if (response.IsSuccessStatusCode)
                            {
                                return Ok(new { status = true, message = "Create successful" });
                            }
                            else
                            {
                                return BadRequest(new { message = "Có lỗi xảy ra khi tạo tiện ích." });
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    return StatusCode(500, new { status = false, message = "Lỗi server"});
                }

            }

        }


    }
}
