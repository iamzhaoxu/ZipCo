using System.Collections.Generic;

namespace ZipCo.Users.WebApi.Responses
{
    public class ApiPaginationResponse<T>: ApiSimpleResponse<IEnumerable<T>>
    {
        /// <summary>
        /// Total page number for the current list query
        /// </summary>
        public int TotalPageNumber { get; set; }
    }
}
