namespace OrderApi.Models.Extensions
{
    public class PaginatedResult<T>
    {
        public ICollection<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize {  get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedResult()
        {
            Items = new List<T>();
        }

    }
}
