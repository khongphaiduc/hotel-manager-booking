namespace API_BookingHotel.ViewModels
{
    public class ViewRoomDetail
    {
         
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public string  Status { get; set; }=null!;
        public string? Description { get; set; }
        public string? PathImage { get; set; }
        public decimal Price { get; set; }
        public string MaxGuests { get; set; }
      
    }
}
