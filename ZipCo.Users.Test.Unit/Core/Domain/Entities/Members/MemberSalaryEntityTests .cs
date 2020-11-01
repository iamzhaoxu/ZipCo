using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Test.Unit.Core.Domain.Entities.Members
{
    public class MemberSalaryEntityTests
    {

        [Theory]
        [InlineData(100, FrequencyIds.Month, 100)]
        [InlineData(2400, FrequencyIds.Annual, 200)]
        public void GivenMemberSalaryEntity_WhenCallGetGetMonthlyExpense_ShouldReturnValue(decimal amount, FrequencyIds frequencyId, decimal expectedResult)
        {
            // assign
            var memberExpensive = new MemberSalary
            {
                Amount = amount,
                PayFrequencyId = (long)frequencyId
            };

            // act
            var result = memberExpensive.GetMonthlySalary();

            //  assert
            result.ShouldBe(expectedResult);
        }

    }
}
