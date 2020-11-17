using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZipCo.Users.Application.Responses;

namespace ZipCo.Users.Infrastructure.Persistence.Extensions
{
    public  static class QueryExtension
    {
        public static IQueryable<T> WhereWhenValueNotNull<T>(this IQueryable<T> query, object value, Expression<Func<T, bool>> expression)
        {
            return value != null ? query.Where(expression) : query;
        }

        public static async Task<PaginationResponse<T>> ListByPaging<T>(this IQueryable<T> queryable, int pageSize, int page) 
        {
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);
            var allResult = await queryable.CountAsync();
            var totalPageNumber = (int)Math.Ceiling((double)allResult / pageSize);
            if (totalPageNumber == 0)
            {
                return new PaginationResponse<T>
                {
                    TotalPageNumber = totalPageNumber,
                    Data = new List<T>()
                };
            }
            if (totalPageNumber < page)
            {
                page = totalPageNumber;
            }
            var skip = (page - 1) * pageSize;
            var result = await queryable.Skip(skip).Take(pageSize).ToListAsync();
            return new PaginationResponse<T>
            {
                TotalPageNumber = totalPageNumber,
                Data = result
            };
        }

    }
}
