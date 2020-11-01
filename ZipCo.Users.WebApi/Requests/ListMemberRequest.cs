
namespace ZipCo.Users.WebApi.Requests
{
    public class ListAccountsRequest
    {
        /// <summary>
        /// (Optional) List accounts by account status field
        /// Status can be 'active' or 'closed' 
        /// </summary>
        public string AccountStatus { get; set; }
        /// <summary>
        /// (Optional) List accounts by account number field
        /// Account number is started with 'ZIP'
        /// </summary>
        public string AccountNumber { get; set;  }
        public ApiPaginationRequest Pagination { get; set; } = new ApiPaginationRequest();

    }
}
