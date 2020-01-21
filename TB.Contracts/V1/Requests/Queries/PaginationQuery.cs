namespace TB.Contracts.V1.Requests.Queries
{
    public class PaginationQuery
    {
        const int maxPageSize = 100;
        public PaginationQuery()
        {
            PageNumber = 1;//to const?
            PageSize = 100;//to const?
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize<maxPageSize ? pageSize : maxPageSize;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
