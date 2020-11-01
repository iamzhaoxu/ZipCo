using FluentValidation;
using ZipCo.Users.Application.Requests.Members.Queries;

namespace ZipCo.Users.Application.Validators.Members
{
    public class ListMembersValidator : AbstractValidator<ListMembersQuery>
    {
        public ListMembersValidator()
        {
            RuleFor(c => c.Pagination)
                .PaginationRequestValid();
        }
    }
}
