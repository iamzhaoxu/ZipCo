using System;
using System.Linq;
using Shouldly;
using Xunit;
using ZIpCo.Utility.Formatting.Extensions;

namespace ZipCo.Users.Test.Unit.Utility
{
    public class ExceptionFormattingTests
    {
        [Fact]
        public void GivenExceptionFormatting_WhenCallFlatException_IfExceptionIsAggregateException_ShouldReturn()
        {
            // assign
            Exception aggEx = new AggregateException(new Exception[]
            {
                new Exception("1"),
                new Exception("2"),
            });
            
            // act
            var exceptions = aggEx.FlatException();

            // assert
            exceptions.Count().ShouldBe(2);
        }

        [Fact]
        public void GivenExceptionFormatting_WhenCallFlatException_IfExceptionIsNotAggregateException_ShouldReturn()
        {
            // assign
            var ex = new Exception();

            // act
            var exceptions = ex.FlatException();

            // assert
            exceptions.Count().ShouldBe(1);
        }
    }
}
