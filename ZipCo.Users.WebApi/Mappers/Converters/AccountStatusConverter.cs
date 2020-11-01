using System;
using AutoMapper;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.Domain.Entities.Accounts;

namespace ZipCo.Users.WebApi.Mappers.Converters
{
    public class AccountStatusConverter : IValueConverter<string, AccountStatusIds?>
    {
        public AccountStatusIds? Convert(string accountStatus, ResolutionContext context)
        {
            if (accountStatus == null)
            {
                return null;
            }
            if (!Enum.TryParse(accountStatus.Trim(), true, out AccountStatusIds accountStatusId))
            {
                var error = "Invalid account status";
                throw new BusinessException(error, BusinessErrors.BadRequest(error));
            }

            return accountStatusId;
        }
    }
}
