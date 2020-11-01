
namespace ZipCo.Users.WebApi.Requests
{
    public class ListMemberRequest
    {
        /// <summary>
        /// (Optional) List accounts by member name.
        /// </summary>
        public string MemberName { get; set; }

        public ApiPaginationRequest Pagination { get; set; } = new ApiPaginationRequest();

    }
}
