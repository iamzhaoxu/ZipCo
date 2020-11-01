using MediatR;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Application.Requests.Members.Queries
{
    public class GetMemberByEmailQuery: IRequest<SimpleResponse<Member>>
    {
        public string Email { get; set; }
    }
}
