namespace API_BookingHotel.ViewModels
{
    public class PaginationResult <T>
    {

        public List<T> Data { get; set; } = new();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; } // Tổng số items trong toàn bộ database
        public int TotalPages { get; set; }  // Tổng  số trang  
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }

        public PaginationResult(List<T> data, int totalItem, int currentPage, int pageSize)
        {
            Data = data;
            TotalCount = totalItem;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)totalItem / pageSize);
            HasPrevious = currentPage > 1;
            HasNext = currentPage < TotalPages;
        }
    }
}
