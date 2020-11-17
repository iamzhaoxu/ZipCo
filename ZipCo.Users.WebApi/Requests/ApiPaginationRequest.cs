namespace ZipCo.Users.WebApi.Requests
{
    public class ApiPaginationRequest
    {

        /// <summary>
        /// The number of items displayed in one page.
        /// </summary>
        public int PageSize { get; set; } = 5;

        /// <summary>
        /// Page number for display items.
        /// </summary>
        public int PageNumber { get; set; } = 1;
    }
}
