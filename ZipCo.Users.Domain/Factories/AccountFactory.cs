using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Interfaces;

namespace ZipCo.Users.Domain.Factories
{

    public class AccountFactory : IAccountFactory
    {
        public Account Create(string accountNumber, long memberId)
        {
            var account = new Account
            {
                AccountNumber = accountNumber,
                AccountStatusId = (long)AccountStatusIds.Active,
                AccountBalance = 0,
                PendingBalance = 0
            };
            account.MemberAccounts.Add(new MemberAccount
            {
                AccountId = account.Id,
                MemberId = memberId
            });
            return account;
        }
    }
}
