using System.Collections.Generic;
using AutoMapper;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Requests.Accounts.Commands;
using ZipCo.Users.Application.Requests.Accounts.Queries;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Test.Shared;
using ZipCo.Users.Test.Shared.ApiRequestBuilders;
using ZipCo.Users.Test.Shared.DataBuilders;
using ZipCo.Users.WebApi.Models;

namespace ZipCo.Users.Test.Unit.Presentation.Mappers
{
    public class AccountMapperTests
    {
        private static IMapper _mapper;

        public AccountMapperTests()
        {
            _mapper ??= AutoMapperProvider.Get();
        }

        [Theory]
        [InlineData(1, AccountStatusIds.Active, 3000, -1000, "ZIP000000003")]
        [InlineData(2, AccountStatusIds.Closed, 3000, 0, "ZIP000000103")]
        [InlineData(3, AccountStatusIds.Closed, 3000, 0, null)]
        public void GivenAccountMapper_WhenMapAccountToAccountModel_ShouldMapSuccessful(long id,
            AccountStatusIds accountStatusId,
            decimal accountBalance, decimal pendingBalance,
            string accountNumber)
        {
            // assign
            var memberAccounts = new List<MemberAccount>
            {
                MemberDataBuilder.CreateMemberAccount(1, id, 2),
            };
            var account = AccountDataBuilder.CreateAccount(id, memberAccounts, accountStatusId,
                accountBalance, pendingBalance, accountNumber);
            account.AccountStatus = new AccountStatus
            {
                Id = (long) accountStatusId,
                Name = accountStatusId.ToString()
            };
            // act
            var accountModel = _mapper.Map<AccountModel>(account);

            // assert
            accountModel.ShouldNotBeNull();
            accountModel.ShouldSatisfyAllConditions(
                () => accountModel.AccountNumber.ShouldBe(account.AccountNumber),
                () => accountModel.AccountStatus.ShouldBe(account.AccountStatus.Name),
                () => accountModel.Id.ShouldBe(account.Id),
                () => accountModel.AccountBalance.ShouldBe(account.AccountBalance),
                () => accountModel.PendingBalance.ShouldBe(account.PendingBalance),
                () => accountModel.AvailableBalance.ShouldBe(account.AccountBalance + account.PendingBalance));
        }


        [Fact]
        public void GivenAccountMapper_WhenMapListAccountRequestToListAccountQuery_ShouldMapSuccessful()
        {
            // assign
            var pagination = CommonRequestBuilder.CreateApiPaginationRequest(10, 3);
            var listAccountRequest = AccountRequestBuilder.CreateListAccountsRequest("active", "ZIP10000001", pagination);
  
            // act
            var listAccountQuery = _mapper.Map<ListAccountsQuery>(listAccountRequest);

            // assert
            listAccountQuery.ShouldSatisfyAllConditions(
                () => listAccountQuery.ShouldNotBeNull(),
                () => listAccountQuery.AccountNumber.ShouldBe(listAccountRequest.AccountNumber),
                () => listAccountQuery.AccountStatusId.ToString().ToLower().ShouldBe(listAccountRequest.AccountStatus.ToLower()),
                () => listAccountQuery.Pagination.ShouldNotBeNull(),
                () => listAccountQuery.Pagination.PageSize.ShouldBe(listAccountRequest.Pagination.PageSize),
                () => listAccountQuery.Pagination.PageNumber.ShouldBe(listAccountRequest.Pagination.PageNumber));
        }


        [Fact]
        public void GivenAccountMapper_WhenMapSignUpAccountRequestToSignUpAccountCommand_ShouldMapSuccessful()
        {
            // assign
            var signUpAccountRequest = AccountRequestBuilder.CreateSignUpAccountRequest(133);

            // act
            var signUpAccountCommand = _mapper.Map<SignUpAccountCommand>(signUpAccountRequest);

            // assert
            signUpAccountRequest.ShouldSatisfyAllConditions(
                () => signUpAccountCommand.ShouldNotBeNull(),
                () => signUpAccountCommand.MemberId.ShouldBe(signUpAccountRequest.MemberId)
            );
        }
    }
}
