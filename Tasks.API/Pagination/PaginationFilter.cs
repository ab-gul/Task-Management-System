namespace Tasks.API.Pagination
{
    public class PaginationFilter
    {
        private int maxPageSize = 10;
        private int minPageNumber = 1;

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public PaginationFilter() : this(null, null)
        {

        }

        public PaginationFilter(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber == null || pageNumber < minPageNumber
                ? minPageNumber
                : (int)pageNumber;

            this.PageSize = pageSize == null || pageSize > maxPageSize
                ? maxPageSize
                : (int)pageSize;

        }
    }
}
