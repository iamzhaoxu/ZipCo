using System.Collections.Generic;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Domain.Entities.Accounts
{
    public class Account : AudibleBaseEntity
    {
        public string AccountNumber { get; set; }

        public long AccountStatusId { get; set; }

        public AccountStatus AccountStatus { get; set; }

        public decimal PendingBalance { get; set; }

        public decimal AccountBalance { get; set; }

        public ICollection<MemberAccount> MemberAccounts { get; set; } = new List<MemberAccount>();

        public decimal AvailableBalance()
        {
            return AccountBalance + PendingBalance;
        }

    }
}
