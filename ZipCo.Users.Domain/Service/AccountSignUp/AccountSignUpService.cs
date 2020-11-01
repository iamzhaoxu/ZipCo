using System.Collections.Generic;
using ZipCo.Users.Domain.Entities.AccountSignUp;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Interfaces;

namespace ZipCo.Users.Domain.Service.AccountSignUp
{
    public class AccountSignUpService : IAccountSignUpService
    {
        private readonly IEnumerable<IAccountSignUpGuard> _signUpGuards;

        public AccountSignUpService(IEnumerable<IAccountSignUpGuard> signUpGuards)
        {
            _signUpGuards = signUpGuards;
        }

        public AccountSignUpResult Evaluate(Member member, AccountSignUpStrategy strategy)
        {
            foreach (var accountSignUpGuard in _signUpGuards)
            {
                var result = accountSignUpGuard.CanSignUp(member, strategy);
                if (!result.IsSuccess)
                {
                    return result;
                }
            }
            return AccountSignUpResult.Success();
        }
    }
}
