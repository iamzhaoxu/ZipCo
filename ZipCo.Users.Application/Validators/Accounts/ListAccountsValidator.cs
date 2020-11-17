using System.Text.RegularExpressions;
using FluentValidation;
using ZipCo.Users.Application.Requests.Accounts.Queries;

namespace ZipCo.Users.Application.Validators.Accounts
{
    public class ListAccountsValidator : AbstractValidator<ListAccountsQuery>
    {
        public ListAccountsValidator()
        {
            RuleFor(c => c.Pagination)
                .PaginationRequestValid();

            RuleFor(c => c.AccountNumber)
                .Matches(new Regex("^ZIP\\d+$", RegexOptions.IgnoreCase))
                .WithMessage(ValidationTokens.InvalidAccountNumber)
                .When(c => !string.IsNullOrEmpty(c.AccountNumber));
        }
    }
}
