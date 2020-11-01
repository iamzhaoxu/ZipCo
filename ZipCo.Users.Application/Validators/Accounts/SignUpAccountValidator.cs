using FluentValidation;
using ZipCo.Users.Application.Requests.Accounts.Commands;

namespace ZipCo.Users.Application.Validators.Accounts
{
    public class SignUpAccountValidator : AbstractValidator<SignUpAccountCommand>
    {
        public SignUpAccountValidator()
        {
            RuleFor(c => c.MemberId)
                .Must(id => id > 0)
                .WithMessage(ValidationTokens.InvalidMemberId);
        }
    }
}
