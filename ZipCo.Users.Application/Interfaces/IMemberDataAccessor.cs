using System.Threading.Tasks;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Application.Interfaces
{
    public interface IMemberDataAccessor
    {
        Task<Member> GetById(long memberId);
        Task<Member> GetByEmail(string email);
        Task<PaginationResponse<Member>> ListAll(PaginationRequest paginationRequest, string searchMemberName);
        Task Create(Member member);
    }

}
