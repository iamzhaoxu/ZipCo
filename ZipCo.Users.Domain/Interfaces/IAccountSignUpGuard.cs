using ZipCo.Users.Domain.Entities.AccountSignUp;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Service.AccountSignUp;

namespace ZipCo.Users.Domain.Interfaces
{
    public interface IAccountSignUpGuard
    {
        public AccountSignUpResult CanSignUp(Member member, AccountSignUpStrategy strategy);
    }
}
