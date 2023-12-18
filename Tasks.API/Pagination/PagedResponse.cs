using Microsoft.AspNetCore.Mvc;

namespace Tasks.API.Pagination
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }


        private PagedResponse(IEnumerable<T> items,int pageNumber, int pageSize, int totalPages)
        {
            this.Items = items;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalPages = totalPages;
        }


        public static PagedResponse<T> Create(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return new PagedResponse<T>(items, pageNumber, pageSize, totalPages);
        }
    }
}
