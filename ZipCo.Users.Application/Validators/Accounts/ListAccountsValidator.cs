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
        }
    }
}
