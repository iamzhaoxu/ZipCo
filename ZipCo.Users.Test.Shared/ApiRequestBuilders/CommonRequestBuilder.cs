using ZipCo.Users.WebApi.Requests;

namespace ZipCo.Users.Test.Shared.ApiRequestBuilders
{
    public static class CommonRequestBuilder
    {
        public static ApiPaginationRequest CreateApiPaginationRequest(int pageNumber, int pageSize)
        {
            return new ApiPaginationRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

    }
}
