using API_BookingHotel.Modules.WorkWithFIles;
using API_BookingHotel.ViewModels;
using Microsoft.EntityFrameworkCore;
using Mydata.Models;
using MyData.Models;

namespace API_BookingHotel.Modules.Rooms.RoomsService
{
    public class EditRoom : IEditableRoom
    {
        private readonly ManagermentHotelContext _dbcontext;
        private readonly IMyFiles _file;

        public EditRoom(ManagermentHotelContext context, IMyFiles file)
        {
            _dbcontext = context;
            _file = file;
        }

        public async Task<bool> EditRoomStatus(AdJustRoom room)
        {

            var item = _dbcontext.Rooms.FirstOrDefault(r => r.RoomId == room.RoomId);

            if (item != null)
            {
                item.RoomTypeId = room.RoomTypeId;
                item.RoomNumber = room.RoomNumber;
                item.Floor = room.Floor;
                item.Description = room.Description;
                item.PricePrivate = room.PricePerNight;
            }


            if (room.DeletedImageIds != null && room.DeletedImageIds.Any())  // kiểm tra list trước khi duyệt
            {
                foreach (var imageId in room.DeletedImageIds)
                {
                    var image = await _dbcontext.Images.FirstOrDefaultAsync(i => i.IdImage == imageId && i.IdRoom == room.RoomId);
                    if (image != null)
                    {
                        _dbcontext.Images.Remove(image);
                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                        string filePath = Path.Combine(folderPath, image.LinkImage);

                        if (File.Exists(filePath)) // check xem ảnh có tồn tại ở server k 
                        {
                            File.Delete(filePath); // xóa
                        }
                    }
                }
            }


            if (room.NewImages != null && room.NewImages.Any())  // kiểm tra list trước khi duyệt
            {
                foreach (var image in room.NewImages)
                {
                    string path = Path.Combine("wwwroot", "images");

                    string NameImage = await _file.SaveFiles(image, path);    // lưu ảnh vào folder và lấy tên ảnh

                    _dbcontext.Images.Add(new Images { LinkImage = NameImage, IdRoom = room.RoomId });

                }
            }

            return await _dbcontext.SaveChangesAsync() > 0;

        }

        public async Task<AdJustRoom> GetFullInfoRoom(int roomId, string apihost)
        {
            var allRoomTypes = await _dbcontext.RoomTypes
                .Select(rt => new RoomTypeViewModel
                {
                    RoomTypeId = rt.RoomTypeId,
                    TypeName = rt.Name
                }).ToListAsync();


            var item = await _dbcontext.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.RoomAmenities)
                .ThenInclude(ra => ra.Amenity)
                .Include(r => r.Images).Where(s => s.RoomId == roomId).Select(s => new AdJustRoom()
                {

                    RoomId = roomId,
                    RoomTypeId = s.RoomTypeId,
                    RoomNumber = s.RoomNumber,
                    Floor = s.Floor ?? 1,
                    PricePerNight = s.PricePrivate != 0 ? s.PricePrivate : s.RoomType.Price,  // nếu có giá giêng thì lấy khong thì lấy theo loại phòng
                    Description = s.Description,


                    AllRoomTypes = allRoomTypes,

                    CurrentAmenities = s.RoomAmenities.Select(s => new AmenityViewModel()
                    {
                        Id = s.AmenityId,
                        Name = s.Amenity.Name,


                    }).ToList(),


                    CurrentImages = s.Images.Select(s => new ImageViewModel()
                    {
                        Id = s.IdImage,
                        Url = s.LinkImage.StartsWith("http") ? s.LinkImage : $"{apihost}/images/" + s.LinkImage

                    }).ToList()

                }).FirstOrDefaultAsync();

            return item;

        }
    }
}
