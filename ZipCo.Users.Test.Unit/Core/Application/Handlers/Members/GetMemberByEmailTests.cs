using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Handlers.Members;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Test.Shared.DataBuilders;

namespace ZipCo.Users.Test.Unit.Core.Application.Handlers.Members
{
    public class GetMemberByEmailTests
    {
        private readonly GetMemberByEmailHandler _getMemberByEmailHandler;
        private readonly IMemberService _memberService;

        public GetMemberByEmailTests()
        {
            _memberService = Substitute.For<IMemberService>();
            _getMemberByEmailHandler = new GetMemberByEmailHandler(_memberService);
        }

        [Fact]
        public async Task GivenGetMemberByEmailHandler_WhenCallHandle_ShouldReturn()
        {
            // assign
            var query = new GetMemberByEmailQuery()
            {
                Email = "jack@zip.test.com"
            };
            var member = MemberDataBuilder.CreateMember(1, null, null, null);
            _memberService.GetMemberByEmail(query.Email).Returns(member);

            // act
            var response = await _getMemberByEmailHandler.Handle(query, CancellationToken.None);

            // assert

            response.ShouldNotBeNull();
            response.Data.ShouldBe(member);
        }
    }
}
