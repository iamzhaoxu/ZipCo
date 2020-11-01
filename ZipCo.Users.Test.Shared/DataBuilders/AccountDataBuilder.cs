using System;
using System.Collections.Generic;
using System.Linq;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Test.Shared.DataBuilders
{
    public static class AccountDataBuilder
    {
        public static Account CreateAccount(
            long id,
            IEnumerable<MemberAccount> memberAccounts,
            AccountStatusIds accountStatusId = AccountStatusIds.Active,
            decimal accountBalance = 10000,
            decimal pendingBalance = 2000,
            string accountNumber = "ZIP00000001")
        {
            memberAccounts ??= new MemberAccount[] { };

            return new Account
            {
                Id = id,
                AccountStatusId = (long)accountStatusId,
                AccountBalance = accountBalance,
                PendingBalance = pendingBalance,
                AccountNumber = accountNumber,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                MemberAccounts = memberAccounts.ToArray()
            };
        }

    }
}
