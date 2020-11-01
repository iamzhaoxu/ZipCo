using AutoMapper;
using ZipCo.Users.Application.Requests.Members.Commands;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.WebApi.Models;
using ZipCo.Users.WebApi.Requests;

namespace ZipCo.Users.WebApi.Mappers
{
    public class MemberMapper: Profile
    {
        public MemberMapper()
        {
            #region Model

            CreateMap<Member, MemberModel>()
                .ForMember(dest => dest.MonthlyExpense, opt => opt.MapFrom(src => src.MemberExpense.GetMonthlyExpense()))
                .ForMember(dest => dest.MonthlySalary, opt => opt.MapFrom(src => src.MemberSalary.GetMonthlySalary()));

            #endregion

            #region Command

            CreateMap<SignUpMemberRequest, SignUpMemberCommand>();

            #endregion

            #region Query

            CreateMap<string, GetMemberByEmailQuery>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src));

            CreateMap<ListMemberRequest, ListMembersQuery>();

            #endregion


        }
    }
}
