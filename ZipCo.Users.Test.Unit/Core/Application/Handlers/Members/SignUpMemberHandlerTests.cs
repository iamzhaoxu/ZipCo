using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Handlers.Members;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests.Members.Commands;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Interfaces;
using ZipCo.Users.Test.Shared.DataBuilders;

namespace ZipCo.Users.Test.Unit.Core.Application.Handlers.Members
{
    public class SignUpMemberHandlerTests
    {
        private readonly SignUpMemberHandler _signUpMemberHandler;
        private readonly IMemberFactory _memberFactory;
        private readonly IMemberService _memberService;
        private readonly IUserUnitOfWork _unitOfWork;

        public SignUpMemberHandlerTests()
        {
            _memberFactory = Substitute.For<IMemberFactory>(); 
            _unitOfWork = Substitute.For<IUserUnitOfWork>();
            _memberService = Substitute.For<IMemberService>();
            _signUpMemberHandler = new SignUpMemberHandler(_memberService, _memberFactory, _unitOfWork);
        }

       

        [Fact]
        public async Task GivenSignUpMemberHandler_WhenCallHandle_IfMemberFound_ShouldSignUpMember()
        {
            // assign
            var query = new SignUpMemberCommand
            {
                Name = "jack",
                Email = "jack@zip.test.com",
                MonthlySalary = 1000,
                MonthlyExpense = 3000
            };
            var member = MemberDataBuilder.CreateMember(1, null, null, null);
            _memberFactory.Create(query.Email, query.Name, query.MonthlyExpense, query.MonthlySalary).Returns(member);
            _memberService.SignUpMember(Arg.Any<Member>()).Returns(member);
            _memberService.GetMemberById(Arg.Any<long>()).Returns(member);
            // act
            var response = await _signUpMemberHandler.Handle(query, CancellationToken.None);

            // assert
            await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
            response.ShouldNotBeNull();
            response.Data.ShouldBe(member);
        }
    }
}
