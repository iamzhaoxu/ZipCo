using System;
using System.Collections.Generic;

namespace ZIpCo.Utility.Formatting.Extensions
{
    public static class ExceptionFormatting
    {
        public static IEnumerable<Exception> FlatException(this Exception ex)
        {
            if (ex is AggregateException aggregateException)
            {
                return aggregateException.Flatten().InnerExceptions;
            }
            return new[] {ex};
        }
    }
}
