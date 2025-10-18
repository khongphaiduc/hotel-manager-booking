using Management_Hotel_2025.ViewModel;
using Microsoft.EntityFrameworkCore;
using Mydata.Models;

namespace Management_Hotel_2025.Modules.Rooms.ManagementRoom
{
    public class ManagementBooking : IManagementBooking
    {
        private readonly ManagermentHotelContext _Dbcontext;

        public ManagementBooking(ManagermentHotelContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }

        public List<BookingItem> GetListBooking(DateTime DateStart, DateTime EndDate)
        {

            if (DateStart == null || EndDate == null)
            {
                DateStart = DateTime.Now.AddMonths(-1);
                EndDate = DateTime.Now.AddMonths(1);
            }
            else
            {
                var List = _Dbcontext.Bookings.Include(s => s.BookingDetails).Where(s => s.BookingDate >= DateStart && s.BookingDate <= EndDate)
                                         .Select(s => new BookingItem()
                                         {

                                             Code = s.BookingCode,
                                             CustomerName = s.CustomerName,
                                             Phone = s.CustomerPhone,
                                             Email = s.Email,
                                             CheckIn = (DateTime)s.BookingDetails.Select(x => x.CheckInDate).FirstOrDefault(),
                                             CheckOut = (DateTime)s.BookingDetails.Select(x => x.CheckOutDate).FirstOrDefault(),
                                             RoomCount = s.BookingDetails.Count,
                                             Status = s.Status,
                                             BookingDate = s.BookingDate,


                                         }).ToList();

                return List;
            }

            return new List<BookingItem>();

        }

        // search by code booking 
        public List<BookingItem> SearchByBookingCode(string search)
        {
            search = search.Trim().ToLower();
            var List = _Dbcontext.Bookings.Include(s => s.BookingDetails).Where(s => s.BookingCode.Equals(search))
                                    .Select(s => new BookingItem()
                                    {

                                        Code = s.BookingCode,
                                        CustomerName = s.CustomerName,
                                        Phone = s.CustomerPhone,
                                        Email = s.Email,
                                        CheckIn = (DateTime)s.BookingDetails.Select(x => x.CheckInDate).FirstOrDefault(),
                                        CheckOut = (DateTime)s.BookingDetails.Select(x => x.CheckOutDate).FirstOrDefault(),
                                        RoomCount = s.BookingDetails.Count,
                                        Status = s.Status,
                                        BookingDate = s.BookingDate,


                                    }).ToList();
            return List;
        }

        //  search by name 
        public List<BookingItem> SearchByBookingName(string search)
        {
            search = search.Trim().ToLower();
            var List = _Dbcontext.Bookings.Include(s => s.BookingDetails).Where(s => s.CustomerName.Contains(search))
                                    .Select(s => new BookingItem()
                                    {

                                        Code = s.BookingCode,
                                        CustomerName = s.CustomerName,
                                        Phone = s.CustomerPhone,
                                        Email = s.Email,
                                        CheckIn = (DateTime)s.BookingDetails.Select(x => x.CheckInDate).FirstOrDefault(),
                                        CheckOut = (DateTime)s.BookingDetails.Select(x => x.CheckOutDate).FirstOrDefault(),
                                        RoomCount = s.BookingDetails.Count,
                                        Status = s.Status,
                                        BookingDate = s.BookingDate,


                                    }).ToList();
            return List;
        }


        // search by phone
        public List<BookingItem> SearchByBookingPhone(string search)
        {
            search = search.Trim().ToLower();
            var List = _Dbcontext.Bookings.Include(s => s.BookingDetails).Where(s => s.CustomerPhone.Equals(search))
                                    .Select(s => new BookingItem()
                                    {

                                        Code = s.BookingCode,
                                        CustomerName = s.CustomerName,
                                        Phone = s.CustomerPhone,
                                        Email = s.Email,
                                        CheckIn = (DateTime)s.BookingDetails.Select(x => x.CheckInDate).FirstOrDefault(),
                                        CheckOut = (DateTime)s.BookingDetails.Select(x => x.CheckOutDate).FirstOrDefault(),
                                        RoomCount = s.BookingDetails.Count,
                                        Status = s.Status,
                                        BookingDate = s.BookingDate,


                                    }).ToList();
            return List;
        }


        // search by email
        public List<BookingItem> SearchByBookingEmail(string search)
        {
            search = search.Trim().ToLower();
            var List = _Dbcontext.Bookings.Include(s => s.BookingDetails).Where(s => s.Email.Equals(search))
                                    .Select(s => new BookingItem()
                                    {

                                        Code = s.BookingCode,
                                        CustomerName = s.CustomerName,
                                        Phone = s.CustomerPhone,
                                        Email = s.Email,
                                        CheckIn = (DateTime)s.BookingDetails.Select(x => x.CheckInDate).FirstOrDefault(),
                                        CheckOut = (DateTime)s.BookingDetails.Select(x => x.CheckOutDate).FirstOrDefault(),
                                        RoomCount = s.BookingDetails.Count,
                                        Status = s.Status,
                                        BookingDate = s.BookingDate,


                                    }).ToList();
            return List;
        }



        public List<BookingItem> GetListBooking(string search)
        {


            var list1 = SearchByBookingCode(search);
            var list2 = SearchByBookingName(search);
            var list3 = SearchByBookingPhone(search);
            var list4 = SearchByBookingEmail(search);

            if (list1.Count > 0)
                return list1;
            else if (list2.Count > 0)
                return list2;
            else if (list3.Count > 0)
                return list3;
            else if (list4.Count > 0)
                return list4;

            return new List<BookingItem>();

        }


    }
}
