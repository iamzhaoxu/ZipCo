using System.Threading.Tasks;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Application.Interfaces
{
    public interface IMemberService
    {
        Task<Member> GetMemberById(long memberId);
        Task<Member> GetMemberByEmail(string email);
        Task<PaginationResponse<Member>> ListMembers(ListMembersQuery listMembersQuery);
        Task<bool> IsMemberExisted(string email);
        Task<Member> SignUpMember(Member member);

    }
}