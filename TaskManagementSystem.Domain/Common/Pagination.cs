namespace TaskManagementSystem.Domain.Common
{
    public class Pagination<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public Pagination(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }


}
