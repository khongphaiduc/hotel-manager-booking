namespace API_BookingHotel.ViewModels
{
    public class PaginationRequest
    {

        public int PageCurrent { get; set; } = 1; // Default to the first page
        public int NumberItemOfPage { get; set; } // Default to 10 items per page
        public PaginationRequest()
        {
        }
        public PaginationRequest(int PageCurrent, int NumberItemOfPage)
        {
            this.PageCurrent = PageCurrent;
            this.NumberItemOfPage = NumberItemOfPage;
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
