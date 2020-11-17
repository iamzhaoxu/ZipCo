namespace ZipCo.Users.Application.Validators
{
    public static class ValidationTokens
    {
        public const string InvalidEmailFormat = "{0} format is not valid.";
        public const string IsRequire = "{0} is required";
        public const string MemberEmail = "Memer email";
        public const string MemberName = "Memer name";
        public const string NegativeMemberSalary = "Monthly salary amount can not be negative";
        public const string MemberSalaryTooHigh = "Monthly salary amount can not be greater than or equal to 1 billon";
        public const string NegativeMemberExpense = "Monthly expense amount can not be negative";
        public const string MemberExpenseTooHigh = "Monthly expense amount can not be greater than or equal to 1 billon";
        public const string InvalidMemberId = "Member Id can not be 0";
        public const string InvalidAccountNumber = "The account number is invalid. Account number should start with 'zip' as prefix and follow with digital numbers.";
        public const string Pagination = "Pagination";
        public const string PageSizeTooSmall = "Page size value should be greater than 0";
        public const string PageSizeTooLarge = "Page size value should be less than 1000";
        public const string PageNumberTooSmall = "Page number value should be greater than 0";
        public const string PageNumberTooLarge = "Page number value should be less than 10000";
    }

}
