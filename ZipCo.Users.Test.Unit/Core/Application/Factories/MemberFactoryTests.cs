
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Factories;
using ZipCo.Users.Domain.Interfaces;

namespace ZipCo.Users.Test.Unit.Core.Application.Factories
{
    public class MemberFactoryTests
    {
        private readonly IMemberFactory _memberFactory;

        public MemberFactoryTests()
        {
            _memberFactory = new MemberFactory();
        }

        [Fact]
        public void GivenAccountFactory_WhenCallCreate_ShouldReturnAccount()
        {
            var testData = (Email: "jacl@zi.com", Name: "jack", MonthlySalary:3012, MonthlyExpense: 3013);
            var member = _memberFactory.Create(testData.Email, testData.Name, testData.MonthlyExpense, testData.MonthlySalary);
            member.ShouldNotBeNull();
            member.ShouldSatisfyAllConditions(
                () => member.Email.ShouldBe(testData.Email),
                () => member.Name.ShouldBe(testData.Name),
                () => member.MemberSalary.Amount.ShouldBe(testData.MonthlySalary),
                () => member.MemberSalary.IsMonthlySalary.ShouldBeTrue(),
                () => member.MemberExpense.Amount.ShouldBe(testData.MonthlyExpense),
                () => member.MemberExpense.IsMonthlyBill.ShouldBeTrue());
        }
    }
}
