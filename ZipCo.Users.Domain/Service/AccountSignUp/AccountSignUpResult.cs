namespace ZipCo.Users.Domain.Service.AccountSignUp
{
    public class AccountSignUpResult
    {
        public bool IsSuccess { get; private set; }
        public string ReasonPhase { get; private set; }
        
        public static AccountSignUpResult Success()
        {
            return new AccountSignUpResult
            {
                IsSuccess = true
            };
        }

        public static AccountSignUpResult Fail(string reasonPhase)
        {
            return new AccountSignUpResult
            {
                IsSuccess = false,
                ReasonPhase = reasonPhase
            };
        }
    }
}
