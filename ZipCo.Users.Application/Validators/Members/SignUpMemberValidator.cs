using FluentValidation;
using ZipCo.Users.Application.Requests.Members.Commands;

namespace ZipCo.Users.Application.Validators.Members
{
    public class SignUpMemberValidator : AbstractValidator<SignUpMemberCommand>
    {
        public SignUpMemberValidator()
        {
            RuleFor(c => c.Name)
                .IsRequired(ValidationTokens.MemberName);

            RuleFor(c => c.Email)
                .EmailRequired(ValidationTokens.MemberEmail);

            RuleFor(c => c.MonthlySalary)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ValidationTokens.NegativeMemberSalary)
                .LessThan(1000 * 1000 * 1000)
                .WithMessage(ValidationTokens.MemberSalaryTooHigh); 

            RuleFor(c => c.MonthlyExpense)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ValidationTokens.NegativeMemberExpense)
                .LessThan(1000 * 1000 * 1000)
                .WithMessage(ValidationTokens.MemberExpenseTooHigh);
        }
    }
}
