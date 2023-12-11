namespace Tasks.API.Pagination
{
    public class PageResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalCount { get; set; }


        public PageResponse(int pageNumber, int pageSize, Uri firstPage, Uri lastPage, int totalCount)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.FirstPage = firstPage;
            this.LastPage = lastPage;
            this.TotalCount = totalCount;

            
        }
    }
}
