namespace API_BookingHotel.ViewModels
{
    public class PaginationRequestAdvance
    {

        public int PageCurrent { get; set; } = 1; // Default to the first page
        public int NumberItemOfPage { get; set; } // Default to 10 items per page

        public int? Floor { get; set; }

        public int? PriceMin { get; set; } 

        public int? PriceMax { get; set; } 

        public int? Person { get; set; } 

        public PaginationRequestAdvance()
        {
        }

        public PaginationRequestAdvance(int pageCurrent, int numberItemOfPage, int floor, int priceMin, int priceMax, int person)
        {
            PageCurrent = pageCurrent;
            NumberItemOfPage = numberItemOfPage;
            Floor = floor;
            PriceMin = priceMin;
            PriceMax = priceMax;
            Person = person;
        }

        public int ValidatePageNumber()
        {
            if (PageCurrent < 1)
            {
                PageCurrent = 1; 
            }
            return PageCurrent;
        }

        public int ValidatePageSize()
        {
            if (NumberItemOfPage < 1)
            {
                NumberItemOfPage = 8; // Default to 10 items per page
            }
            return NumberItemOfPage;
        }

    }
}
