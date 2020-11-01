namespace ZipCo.Users.Domain.Entities.AccountSignUp
{
    /// <summary>
    /// Provide strategy met data which be used for the account sign up.
    /// </summary>
    public class AccountSignUpStrategy : AudibleBaseEntity
    {
        public string Name { get; set; }
        public decimal? MonthNetIncomeLimit { get; set; }
        public  bool IsDefault { get; set; }
    }
}
