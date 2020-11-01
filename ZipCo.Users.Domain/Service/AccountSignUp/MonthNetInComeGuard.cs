using ZipCo.Users.Domain.Entities.AccountSignUp;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Interfaces;

namespace ZipCo.Users.Domain.Service.AccountSignUp
{
    public class MonthNetIncomeGuard : IAccountSignUpGuard
    {
        public AccountSignUpResult CanSignUp(Member member, AccountSignUpStrategy strategy)
        {
            if (member.GetMonthNetIncome() < strategy.MonthNetIncomeLimit)
            {
                return AccountSignUpResult.Fail($"Month net income from member {member.Id} is too low to sign up a account.");
            }
            return AccountSignUpResult.Success();
        }
    }
}
