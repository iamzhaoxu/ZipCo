using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using ZipCo.Users.Infrastructure.Persistence.Extensions;
using ZipCo.Users.Test.Shared.DataBuilders;
using ZipCo.Users.Test.Shared.InMemoryDb;

namespace ZipCo.Users.Test.Unit.Infrastructure.Persistence.Extensions
{
    public class QueryExtensionTests
    {
        [Fact]
        public async Task GivenQueryExtension_WhenCallListByPaging_IfDataSetIsEmpty_ShouldReturnEmptyPaginationResponse()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // act
                var result = await context.AccountSignUpStrategies
                    .OrderBy(a => a.Id)
                    .ListByPaging(1, 1);

                // assert
                result.TotalPageNumber.ShouldBe(0);
                result.Data.Count().ShouldBe(0);
            }
        }

        [Theory]
        [MemberData(nameof(GetListByPagingQueryTestData))]
        public async Task GivenQueryExtension_WhenCallListByPaging_IfDataSetIsNotEmpty_ShouldReturnPaginationResponse(
            int pageSize, int pageNumber, int totalPageNumber, string[] expectedResult)
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                context.AccountSignUpStrategies.Add(AccountSignUpStrategyDataBuilder.CreateAccountSignUpStrategy(10, name:"One"));
                context.AccountSignUpStrategies.Add(AccountSignUpStrategyDataBuilder.CreateAccountSignUpStrategy(11, name: "Two"));
                context.AccountSignUpStrategies.Add(AccountSignUpStrategyDataBuilder.CreateAccountSignUpStrategy(12, name: "Three"));
                context.AccountSignUpStrategies.Add(AccountSignUpStrategyDataBuilder.CreateAccountSignUpStrategy(13, name: "Four"));
                context.AccountSignUpStrategies.Add(AccountSignUpStrategyDataBuilder.CreateAccountSignUpStrategy(14, name: "Five"));
                await context.SaveChangesAsync();

                // act
                var result = await context.AccountSignUpStrategies
                                .OrderBy(a => a.Id)
                                .ListByPaging(pageSize, pageNumber);

                // assert
                result.TotalPageNumber.ShouldBe(totalPageNumber);
                result.Data.Count().ShouldBe(expectedResult.Length);
                result.Data.Select(a => a.Name).Intersect(expectedResult).Count().ShouldBe(expectedResult.Length);
            }
        }

        [Theory]
        [InlineData("you cant find me!!", 0)]
        [InlineData(null, 1)]
        public async Task GivenQueryExtension_WhenWhereValueExists_ShouldReturn(string criteria, int expectedRecordNumber)
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                context.AccountSignUpStrategies.Add(AccountSignUpStrategyDataBuilder.CreateAccountSignUpStrategy(10, name: "One"));
                await context.SaveChangesAsync();

                // act
                var result = await context.AccountSignUpStrategies
                    .WhereWhenValueNotNull(criteria, a => a.Name == criteria)
                    .ToListAsync();

                // assert
                result.Count.ShouldBe(expectedRecordNumber);
            }
        }

        public static IEnumerable<object[]> GetListByPagingQueryTestData()
        {
            yield return new object[] { 2, 1, 3, new []{ "One", "Two" } };
            yield return new object[] { 2, 2, 3, new[] { "Three", "Four" } };
            yield return new object[] { 2, 3, 3, new[] { "Five" } };
            yield return new object[] { 2, 4, 3, new [] { "Five" } };
            yield return new object[] { 2, -1, 3, new[] { "One", "Two" } };
            yield return new object[] { 5, 1, 1, new[] { "One", "Two", "Three", "Four", "Five" } };
            yield return new object[] { 100, 1, 1, new[] { "One", "Two", "Three", "Four", "Five" } };
            yield return new object[] { -1, 1, 5, new[] { "One" } };
        }
        
    }
}
