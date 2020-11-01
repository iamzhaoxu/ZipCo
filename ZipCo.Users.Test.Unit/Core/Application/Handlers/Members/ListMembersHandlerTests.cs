using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Handlers.Members;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Test.Unit.Core.Application.Handlers.Members
{
    public class ListMembersHandlerTests
    {
        private readonly ListMembersHandler _listMembersHandler;
        private readonly IMemberService _memberService;
        public ListMembersHandlerTests()
        {
            _memberService = Substitute.For<IMemberService>();
            _listMembersHandler = new ListMembersHandler(_memberService);
        }

        [Fact]
        public async Task GivenListMembersHandler_WhenCallHandle_ShouldReturn()
        {
            // assign
            var query = new ListMembersQuery
            {
                MemberName = "jack",
                Pagination = new PaginationRequest
                {
                    PageSize = 1,
                    PageNumber = 23
                }
            };
            var result = new PaginationResponse<Member>
            {
                TotalPageNumber = 100,
                Data = new List<Member>()
            };
            _memberService.ListMembers(query).Returns(result);

            // act
            var response = await _listMembersHandler.Handle(query, CancellationToken.None);

            // assert
            response.ShouldNotBeNull();
            response.ShouldBe(result);
        }
    }
}
