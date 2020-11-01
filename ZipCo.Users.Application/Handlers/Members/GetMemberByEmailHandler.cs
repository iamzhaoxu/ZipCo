using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Application.Handlers.Members
{
    public class GetMemberByEmailHandler : IRequestHandler<GetMemberByEmailQuery, SimpleResponse<Member>>
    {
        private readonly IMemberService _memberService;

        public GetMemberByEmailHandler(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<SimpleResponse<Member>> Handle(GetMemberByEmailQuery request,
            CancellationToken cancellationToken)
        {
            var member = await _memberService.GetMemberByEmail(request.Email);
            if (member == null)
            {
                throw new BusinessException("Cannot find member for email {email}",
                    BusinessErrors.ResourceNotFound("member", "Email is not found"),
                    request.Email);
            }
            return SimpleResponse<Member>.Create(member);
        }
    }
}
