using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Application.Handlers.Members
{
    public class ListMembersHandler : IRequestHandler<ListMembersQuery, PaginationResponse<Member>>
    {
        private readonly IMemberService _memberService;

        public ListMembersHandler(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<PaginationResponse<Member>> Handle(ListMembersQuery request, CancellationToken cancellationToken)
        {
            return await _memberService.ListMembers(request);
        }
    }
}
