using ZipCo.Users.WebApi.Requests;

namespace ZipCo.Users.Test.Shared.ApiRequestBuilders
{
    public static class AccountRequestBuilder
    {
        public static ListAccountsRequest CreateListAccountsRequest(string accountStatus, 
            string accountNumber,
            ApiPaginationRequest pagination)
        {
            return new ListAccountsRequest
            {
                AccountStatus = accountStatus,
                AccountNumber = accountNumber,
                Pagination = pagination
            };
        }

        public static SignUpAccountRequest CreateSignUpAccountRequest(long memberId)
        {
            return new SignUpAccountRequest
            {
                MemberId = memberId
            };
        }
    }
}
