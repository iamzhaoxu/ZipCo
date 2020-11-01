using System.Threading.Tasks;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Application.Services.Members
{
    public class MemberService : IMemberService
    {

        private readonly IMemberDataAccessor _memberDataAccessor;

        public MemberService(IMemberDataAccessor memberDataAccessor)
        {
            _memberDataAccessor = memberDataAccessor;
        }

        public async Task<Member> GetMemberById(long memberId)
        {
           return await _memberDataAccessor.GetById(memberId);
        }

        public async Task<Member> GetMemberByEmail(string email)
        {
            return await _memberDataAccessor.GetByEmail(email);
        }

        public async Task<bool> IsMemberExisted(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                var error = "Email is required.";
                throw new BusinessException(error, BusinessErrors.BadRequest(error));
            }
            var member = await _memberDataAccessor.GetByEmail(email);
            return member != null;
        }

        public async Task<Member> SignUpMember(Member member)
        {
            if (!member.IsNew)
            {
                throw new BusinessException("Member {memberId} is not marked as new.",
                    BusinessErrors.Critical("Member is not marked as new."),
                    member.Id);
            }
            var memberExisted =  await IsMemberExisted(member.Email);
            if (memberExisted)
            {
                var error = $"Cannot sign up member for email {member.Email}.";
                throw new BusinessException(error, BusinessErrors.BadRequest(error));
            }
            await _memberDataAccessor.Create(member);
            return member;
        }

        public async Task<PaginationResponse<Member>> ListMembers(ListMembersQuery listMembersQuery)
        {
           return await _memberDataAccessor.ListAll(listMembersQuery.Pagination, listMembersQuery.MemberName);
        }
    }
}
