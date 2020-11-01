using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Application.Services.Members;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Test.Shared.DataBuilders;

namespace ZipCo.Users.Test.Unit.Core.Application.Services.Accounts
{
    public class MemberServiceTests
    {
        private readonly IMemberService _memberService;
        private readonly IMemberDataAccessor _memberDataAccessor;
        public MemberServiceTests()
        {
            _memberDataAccessor = Substitute.For<IMemberDataAccessor>();
            _memberService = new MemberService(_memberDataAccessor);
        }

        [Fact]
        public async Task GivenMemberService_WhenCallGetMemberById_ShouldReturn()
        {
            // assign 
            var member = MemberDataBuilder.CreateMember(1, null, null, null);
            _memberDataAccessor.GetById(member.Id).Returns(member);

            // act
            var actualMember = await _memberService.GetMemberById(member.Id);

            // assert
            actualMember.ShouldBe(member);
        }


        [Fact]
        public async Task GivenMemberService_WhenCallGetMemberByEmail_ShouldReturn()
        {
            // assign 
            var member = MemberDataBuilder.CreateMember(1, null, null, null);
            _memberDataAccessor.GetByEmail(member.Email).Returns(member);

            // act
            var actualMember = await _memberService.GetMemberByEmail(member.Email);

            // assert
            actualMember.ShouldBe(member);
        }

        [Fact]
        public async Task GivenMemberService_WhenCallSignUpMember_IfMemberNotExisted_ShouldCreateMember()
        {
            // assign 
            var member = MemberDataBuilder.CreateMember(0, null, null, null);
            _memberDataAccessor.GetByEmail(member.Email).Returns(default(Member));

            // act
            var signedUpMember = await _memberService.SignUpMember(member);

            // assert
            signedUpMember.ShouldNotBeNull();
            signedUpMember.ShouldBe(member);
            await _memberDataAccessor.Received(1).Create(member);
        }

        [Fact]
        public async Task GivenMemberService_WhenCallSignUpMember_IfMemberExisted_ShouldRaiseException()
        {
            // assign 
            var member = MemberDataBuilder.CreateMember(0, null, null, null);
            _memberDataAccessor.GetByEmail(member.Email).Returns(member);

            // act
            var exception = await Should.ThrowAsync<BusinessException>(() => _memberService.SignUpMember(member));

            // assert
            exception.IsBadRequest.ShouldBeTrue();
            exception.BusinessErrorMessage.ShouldContain($"Cannot sign up member for email {member.Email}");
            await _memberDataAccessor.DidNotReceive().Create(member);
        }

        [Fact]
        public async Task GivenMemberService_WhenCallSignUpMember_IfMemberIdIsNotZero_ShouldRaiseException()
        {
            // assign 
            var member = MemberDataBuilder.CreateMember(1, null, null, null);

            // act
            var exception = await Should.ThrowAsync<BusinessException>(() => _memberService.SignUpMember(member));

            // assert
            exception.IsCritical.ShouldBeTrue();
            exception.BusinessErrorMessage.ShouldContain("Member is not marked as new");
            await _memberDataAccessor.DidNotReceive().Create(member);
        }

        [Fact]
        public async Task GivenMemberService_WhenCallListMember_ShouldReturnMembers()
        {
            // assign 
            var request = new ListMembersQuery
            {
                MemberName = "ja",
                Pagination = new PaginationRequest
                {
                    PageSize = 100,
                    PageNumber = 2
                }
            };
            var response = new PaginationResponse<Member>
            {
                TotalPageNumber = 10,
                Data = new List<Member>()
            };
            _memberDataAccessor.ListAll(request.Pagination, request.MemberName)
                .Returns(response);

            // act
            var actualResponse = await _memberService.ListMembers(request);

            // assert
            actualResponse.ShouldBe(response);
        }

    }
}
