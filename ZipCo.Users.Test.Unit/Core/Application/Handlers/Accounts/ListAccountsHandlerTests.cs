using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Handlers.Accounts;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Requests.Accounts.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Accounts;

namespace ZipCo.Users.Test.Unit.Core.Application.Handlers.Accounts
{
    public class ListAccountsHandlerTests
    {
        private readonly ListAccountsHandler _listAccountsHandler;
        private readonly IAccountService _accountService;
        public ListAccountsHandlerTests()
        {
            _accountService = Substitute.For<IAccountService>();
            _listAccountsHandler = new ListAccountsHandler(_accountService);
        }

        [Fact]
        public async Task GivenListAccountsHandler_WhenCallHandle_ShouldReturn()
        {
            // assign
            var query = new ListAccountsQuery
            {
                AccountStatusId = AccountStatusIds.Active,
                AccountNumber = "123213",
                Pagination = new PaginationRequest
                {
                    PageSize = 1,
                    PageNumber = 23
                }
            };
            var result = new PaginationResponse<Account>
            {
                TotalPageNumber = 100,
                Data = new List<Account>()
            };
            _accountService.ListAccounts(query).Returns(result);

            // act
            var response = await _listAccountsHandler.Handle(query, CancellationToken.None);

            // assert
            response.ShouldNotBeNull();
            response.ShouldBe(result);
        }
    }
}
