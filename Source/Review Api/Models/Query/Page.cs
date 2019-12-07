namespace Review_Api.Models.Query
{
    public class Page
    {
        public int PageNumber;
        public int ItemsPerPage;

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
