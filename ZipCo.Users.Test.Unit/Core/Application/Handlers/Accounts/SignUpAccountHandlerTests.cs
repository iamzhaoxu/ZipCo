using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Handlers.Accounts;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests.Accounts.Commands;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Test.Shared.DataBuilders;

namespace ZipCo.Users.Test.Unit.Core.Application.Handlers.Accounts
{
    public class SignUpMemberHandlerTests
    {
        private readonly SignUpAccountHandler _signUpAccountHandler;
        private readonly IAccountService _accountService;
        private readonly IMemberService _memberService;
        private readonly IUserUnitOfWork _unitOfWork;

        public SignUpMemberHandlerTests()
        {
            _accountService = Substitute.For<IAccountService>();
            _unitOfWork = Substitute.For<IUserUnitOfWork>();
            _memberService = Substitute.For<IMemberService>();

            _signUpAccountHandler = new SignUpAccountHandler(_memberService, _accountService, _unitOfWork);
        }

        [Fact]
        public async Task GivenSignUpAccountHandler_WhenCallHandle_IfMemberNotFound_ShouldRaiseException()
        {
            // assign
            var query = new SignUpAccountCommand
            {
                MemberId = 1
            };
            _memberService.GetMemberById(query.MemberId).Returns(default(Member));

            // act
            var exception = await Should.ThrowAsync<BusinessException>(() => _signUpAccountHandler.Handle(query, CancellationToken.None));

            // assert
            exception.IsBadRequest.ShouldBeTrue();
            exception.BusinessErrorMessage.ShouldContain("Invalid member details.");
            await _accountService.DidNotReceive().SignUpAccount(Arg.Any<Member>());
            await _unitOfWork.DidNotReceive().SaveChangesAsync(CancellationToken.None);
        }

        [Fact]
        public async Task GivenSignUpAccountHandler_WhenCallHandle_IfMemberFound_ShouldSignUpAccount()
        {
            // assign
            var query = new SignUpAccountCommand
            {
                MemberId = 1
            };
            var member = MemberDataBuilder.CreateMember(1, null, null, null);
            var account = AccountDataBuilder.CreateAccount(1, null);
            _memberService.GetMemberById(query.MemberId).Returns(member);
            _accountService.SignUpAccount(member).Returns(account);
            _accountService.GetAccountById(account.Id).Returns(account);

            // act
             var response = await _signUpAccountHandler.Handle(query, CancellationToken.None);

            // assert
            await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
            response.ShouldNotBeNull();
            response.Data.ShouldBe(account);
        }
    }
}
