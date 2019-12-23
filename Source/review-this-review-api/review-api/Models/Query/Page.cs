namespace review_api.Models.Query
{
    public class Page
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }

        public Page(int pageNumber, int itemsPerPage)
        {
            PageNumber = pageNumber;
            ItemsPerPage = itemsPerPage;
        }

        public Page()
        {
        }

        public override string ToString() => $"{PageNumber},{ItemsPerPage}";
    }
}
