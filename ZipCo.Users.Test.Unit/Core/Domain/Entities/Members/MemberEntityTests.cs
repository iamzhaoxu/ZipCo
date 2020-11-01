using System.Collections.Generic;
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Test.Shared.DataBuilders;

namespace ZipCo.Users.Test.Unit.Core.Domain.Entities.Members
{
    public class MemberEntityTests
    {
        [Fact]
        public void GivenMemberEntity_WhenCallGetMonthNetIncome_ShouldReturnValue()
        {
            // assign
            var memberId = 1;
            var memberExpensive = MemberDataBuilder.CreateMemberExpense(1, memberId, FrequencyIds.Month, 2000);
            var memberSalary = MemberDataBuilder.CreateMemberSalary(1, memberId, FrequencyIds.Month, 5000);
            var member = MemberDataBuilder.CreateMember(1, new List<MemberAccount>(), memberExpensive, memberSalary);

            // act
            var result = member.GetMonthNetIncome();

            //  assert
            result.ShouldBe(3000);
        }

    }
}
