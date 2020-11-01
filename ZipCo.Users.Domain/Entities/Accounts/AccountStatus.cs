namespace ZipCo.Users.Domain.Entities.Accounts
{
    public class AccountStatus : BaseEntity
    {
        public string Name { get; set; }
    }

    public enum AccountStatusIds
    {
        Active = 1,
        Closed = 2,
    }
}
