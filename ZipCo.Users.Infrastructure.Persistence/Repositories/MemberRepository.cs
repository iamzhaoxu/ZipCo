using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Infrastructure.Persistence.DataContext;
using ZipCo.Users.Infrastructure.Persistence.Extensions;

namespace ZipCo.Users.Infrastructure.Persistence.Repositories
{

    public class MemberRepository : IMemberDataAccessor
    {
        private readonly UserContext _userContext;

        public MemberRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<Member> GetById(long memberId)
        {
            return await _userContext.Members
                .Include(m => m.MemberExpense.BillFrequency)
                .Include(m => m.MemberSalary.PayFrequency)
                .Where(a => a.Id == memberId)
                .FirstOrDefaultAsync();
        }

        public async Task<Member> GetByEmail(string email)
        {
            return await _userContext.Members
                .Include(m => m.MemberExpense.BillFrequency)
                .Include(m => m.MemberSalary.PayFrequency)
                .Where(m => m.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<PaginationResponse<Member>> ListAll(PaginationRequest paginationRequest, string searchMemberName)
        {
            return await _userContext.Members
                .Include(m => m.MemberExpense.BillFrequency)
                .Include(m => m.MemberSalary.PayFrequency)
                .WhereWhenValueNotNull(searchMemberName, m => m.Name.Contains(searchMemberName))
                .OrderBy(m => m.Name)
                .ThenBy(m => m.CreatedOn)
                .ListByPaging(paginationRequest.PageSize, paginationRequest.PageNumber);
        }

        public async Task Create(Member member)
        {
            await _userContext.Members.AddAsync(member);
        }
    }
}
