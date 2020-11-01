using FluentValidation;
using ZipCo.Users.Application.Requests.Members.Queries;

namespace ZipCo.Users.Application.Validators.Members
{
    public class GetMemberByEmailValidator : AbstractValidator<GetMemberByEmailQuery>
    {
        public GetMemberByEmailValidator()
        {
            RuleFor(c => c.Email)
                .EmailRequired(ValidationTokens.MemberEmail);
        }
    }
}
