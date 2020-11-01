using AutoMapper;
using ZipCo.Users.Application.Requests.Accounts.Commands;
using ZipCo.Users.Application.Requests.Accounts.Queries;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.WebApi.Mappers.Converters;
using ZipCo.Users.WebApi.Models;
using ZipCo.Users.WebApi.Requests;

namespace ZipCo.Users.WebApi.Mappers
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            #region Model

            CreateMap<Account, AccountModel>()
                .ForMember(dest => dest.AccountStatus,
                    opt => opt.MapFrom(src => src.AccountStatus.Name))
                .ForMember(dest => dest.AvailableBalance,
                    opt => opt.MapFrom(src => src.AvailableBalance()));
            #endregion

            #region Command

            CreateMap<SignUpAccountRequest, SignUpAccountCommand>();

            #endregion

            #region Query

            CreateMap<ListAccountsRequest, ListAccountsQuery>()
                .ForMember(dest => dest.Pagination, opt => opt.MapFrom(src => src.Pagination))
                .ForMember(dest => dest.AccountStatusId, opt => opt.ConvertUsing(new AccountStatusConverter(), src => src.AccountStatus));

            #endregion

        }
    }
}
