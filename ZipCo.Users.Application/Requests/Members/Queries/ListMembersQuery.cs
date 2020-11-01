using MediatR;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Application.Requests.Members.Queries
{
    public class ListMembersQuery : IRequest<PaginationResponse<Member>>
    {
        public string MemberName { get; set; }

        public PaginationRequest Pagination { get; set; } = new PaginationRequest();
    }
}
