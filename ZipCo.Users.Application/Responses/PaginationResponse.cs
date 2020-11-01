using System.Collections.Generic;

namespace ZipCo.Users.Application.Responses
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int TotalPageNumber { get; set; }

    }
}
