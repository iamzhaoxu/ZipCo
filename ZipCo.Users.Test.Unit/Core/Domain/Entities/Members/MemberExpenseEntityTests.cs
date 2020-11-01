using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Test.Unit.Core.Domain.Entities.Members
{
    public class MemberExpenseEntityTests
    {
        [Theory]
        [InlineData(100, FrequencyIds.Month, 100)]
        [InlineData(2400, FrequencyIds.Annual, 200)]
        public void GivenMemberExpenseEntity_WhenCallGetGetMonthlyExpense_ShouldReturnValue(decimal amount, FrequencyIds frequencyId, decimal expectedResult)
        {
            // assign
            var memberExpensive = new MemberExpense
            {
                Amount = amount,
                BillFrequencyId = (long)frequencyId
            };

            // act
            var result = memberExpensive.GetMonthlyExpense();

            //  assert
            result.ShouldBe(expectedResult);
        }
    }
}
