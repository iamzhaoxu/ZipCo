using ZipCo.Users.Domain.Entities.Accounts;

namespace ZipCo.Users.Domain.Entities.Members
{
    public class MemberAccount: BaseEntity
    {
        public long MemberId { get; set; }

        public long AccountId { get; set; }

        public  Member Member { get; set; }

        public  Account Account { get; set; }
    }
}
