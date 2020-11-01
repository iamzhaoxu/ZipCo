using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Requests.Accounts.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Application.Services.Accounts;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Domain.Entities.AccountSignUp;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Interfaces;
using ZipCo.Users.Domain.Service.AccountSignUp;
using ZipCo.Users.Test.Shared.DataBuilders;

namespace ZipCo.Users.Test.Unit.Core.Application.Services.Accounts
{
    public class AccountServiceTests
    {
        private readonly IAccountService _accountService;
        private readonly IAccountDataAccessor _accountDataAccessor;
        private readonly IAccountSignUpStrategyDataAccessor _accountSignUpStrategyDataAccessor;
        private readonly IAccountSignUpService _accountSignUpService;
        private readonly IAccountFactory _accountFactory;
        public AccountServiceTests()
        {
            _accountSignUpStrategyDataAccessor = Substitute.For<IAccountSignUpStrategyDataAccessor>(); 
            _accountDataAccessor = Substitute.For<IAccountDataAccessor>();
            _accountSignUpService = Substitute.For<IAccountSignUpService>();
            _accountFactory = Substitute.For<IAccountFactory>();
            _accountService = new AccountService(_accountDataAccessor,
                _accountSignUpStrategyDataAccessor,
                _accountSignUpService,
                _accountFactory);

        }

        [Fact]
        public async Task GivenAccountService_WhenCallSignUpAccount_IfMemberIsQualifiedToSignUp_ShouldRerunAccount()
        {
            // assign 
            var member = MemberDataBuilder.CreateMember(1, null, null, null);
            var evaluatedResult = AccountSignUpResult.Success();
            var account = AccountDataBuilder.CreateAccount(1, null);
            MockAccountSignUpStrategyResult(evaluatedResult, member);
            MockAccountSequenceNumber(100000001);
            _accountFactory.Create("ZIP0100000001", member.Id).Returns(account);

            // act
            var signedUpAccount = await _accountService.SignUpAccount(member);

            // assert
            await _accountDataAccessor.Received(1).Create(signedUpAccount);
            signedUpAccount.ShouldBe(account);
        }

        [Fact]
        public async Task GivenAccountService_WhenCallSignUpAccount_IfMemberIsNoQualifiedToSignUp_ShouldRaiseException()
        {
            // assign 
            var member = MemberDataBuilder.CreateMember(1, null, null, null);
            var evaluatedResult = AccountSignUpResult.Fail("Month net income is too low.");
            MockAccountSignUpStrategyResult(evaluatedResult, member);
            MockAccountSequenceNumber(100000001);

            // act
           var exception = await Should.ThrowAsync<BusinessException>(() => _accountService.SignUpAccount(member));

           // assert
           exception.IsBadRequest.ShouldBeTrue();
           exception.BusinessErrorMessage.ShouldContain(evaluatedResult.ReasonPhase);
           await _accountDataAccessor.DidNotReceive().Create(Arg.Any<Account>());
        }

        [Fact]
        public async Task GivenAccountService_WhenCallListAccount_ShouldReturnAccounts()
        {
            // assign 
            var request = new ListAccountsQuery
            {
                AccountStatusId = AccountStatusIds.Active,
                AccountNumber = "ZIP0111111111",
                Pagination = new PaginationRequest
                {
                    PageSize = 100,
                    PageNumber = 2
                }
            };
            var response =new PaginationResponse<Account>
            {
                TotalPageNumber = 10,
                Data = new List<Account>()
            };
            _accountDataAccessor.ListAll(request.Pagination, request.AccountStatusId, request.AccountNumber)
                .Returns(response);

            // act
            var actualResponse = await _accountService.ListAccounts(request);

            // assert
            actualResponse.ShouldBe(response);
        }


        private void MockAccountSequenceNumber(int sequence)
        {
            _accountDataAccessor.GetNextAccountNumberSeq().Returns(sequence);
        }

        private AccountSignUpStrategy MockAccountSignUpStrategyResult(AccountSignUpResult evaluatedResult, Member member)
        {
            var strategy = AccountSignUpStrategyDataBuilder.CreateAccountSignUpStrategy(1);
            _accountSignUpStrategyDataAccessor.GetDefaultStrategy().Returns(strategy);
            _accountSignUpService.Evaluate(member, strategy).Returns(evaluatedResult);
            return strategy;
        }
    }
}
