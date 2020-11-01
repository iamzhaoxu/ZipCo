using System;
using ZipCo.Users.Domain.Entities.AccountSignUp;

namespace ZipCo.Users.Test.Shared.DataBuilders
{
    public static class AccountSignUpStrategyDataBuilder
    {
        public static AccountSignUpStrategy CreateAccountSignUpStrategy(long id,
            bool isDefault = false,
            decimal monthNetIncomeLimit = 1000,
            string name = "VIP proram")
        {
            return new AccountSignUpStrategy
            {
                Id = id,
                ModifiedOn = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow,
                IsDefault = isDefault,
                MonthNetIncomeLimit = monthNetIncomeLimit,
                Name = name
            };
        }
    }
}
