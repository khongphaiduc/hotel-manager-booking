using System.ComponentModel.DataAnnotations;

namespace Management_Hotel_2025.ViewModel
{
    public class RoomTypeViewModel
    {
        public int RoomTypeId { get; set; }
        public string TypeName { get; set; }
    }

    public class AmenityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }

    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }

    public class AdJustRoom
    {

        public int RoomId { get; set; } // Phải có ID để biết sửa phòng nào

        [Display(Name = "Loại phòng")]
        public int RoomTypeId { get; set; } // Dùng cho <select>

        [Display(Name = "Số phòng")]
        public string RoomNumber { get; set; }

        [Display(Name = "Tầng")]
        public int Floor { get; set; }

        [Display(Name = "Giá mỗi đêm")]
        public decimal PricePerNight { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }



        //Danh sách TÙY CHỌN cho dropdown "Loại phòng"  (đang có )
        public List<RoomTypeViewModel> AllRoomTypes { get; set; } = new List<RoomTypeViewModel>();

        // 3. Tiện ích  
        public List<AmenityViewModel> AllAvailableAmenities { get; set; } = new List<AmenityViewModel>();  // danh sách tất cả tiện ích 
        public List<AmenityViewModel> CurrentAmenities { get; set; } = new List<AmenityViewModel>();  // danh sách tiện ích hiện tại 

        public List<int> DeletedAmenity { get; set; } = new List<int>();                                // danh sách tiện ích cần xóa 


        public List<int> NewAmenities { get; set; } = new List<int>();                                   // danh sách tiện ích thêm mới

        // 4. Ảnh 
        public List<ImageViewModel> CurrentImages { get; set; } = new List<ImageViewModel>();                // hiện đang có 

        public List<int> DeletedImageIds { get; set; } = new List<int>();                                   // thêm để nhận ảnh xóa

        public List<IFormFile> NewImages { get; set; } = new List<IFormFile>();                             // thêm ảnh mới 
                                                                                                            //------------------------------------------------------------------------------------------------

        // danh sách 

        /*
        === Các thuộc tính quan trọng của IFormFile   === 
         FileName: Tên tập tin gốc do client gửi lên.

        ContentType: Kiểu MIME của tập tin, ví dụ "image/jpeg".

        Length: Kích thước tập tin (byte).

        OpenReadStream(): Trả về Stream để đọc nội dung tập tin.

        CopyTo(Stream) / CopyToAsync(Stream): Sao chép nội dung tập tin vào Stream khác, ví dụ lưu vào server.*/
    }
}
