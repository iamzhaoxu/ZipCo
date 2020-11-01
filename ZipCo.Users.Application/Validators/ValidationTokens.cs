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
        public const string InvalidMemberId = "Member Id can not be 0.";
        public const string Pagination = "Pagination.";
        public const string InvalidPageSize = "Page size value should be greater than 0";
        public const string InvalidPageNumber = "Page number value should be greater than 0";
    }

}
