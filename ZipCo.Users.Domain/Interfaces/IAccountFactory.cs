using ZipCo.Users.Domain.Entities.Accounts;

namespace ZipCo.Users.Domain.Interfaces
{
    public interface IAccountFactory
    {
        Account Create(string accountNumber, long memberId);
    }
}
