using System.Collections.Generic;
using AutoMapper;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Requests.Members.Commands;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Test.Shared;
using ZipCo.Users.Test.Shared.ApiRequestBuilders;
using ZipCo.Users.Test.Shared.DataBuilders;
using ZipCo.Users.WebApi.Models;

namespace ZipCo.Users.Test.Unit.Presentation.Mappers
{
    public class MemberMapperTests
    {
        private static IMapper _mapper;

        public MemberMapperTests()
        {
            _mapper ??= AutoMapperProvider.Get();
        }

        [Theory]
        [InlineData(1,  3000, 7000, "jack", "jack@zip.test.com")]
        [InlineData(1, 3000, 7000, null, null)]
        public void GivenMemberMapper_WhenMapMemberToMemberModel_ShouldMapSuccessful(long id,
            decimal monthlyExpense, decimal monthlySalary,
            string name, string emailAddress)
        {
            // assign
            var memberAccounts = new List<MemberAccount>
            {
                MemberDataBuilder.CreateMemberAccount(1, id, 2),
            };
            var memberExpense = MemberDataBuilder.CreateMemberExpense(1, id, FrequencyIds.Month, monthlyExpense);
            var memberSalary = MemberDataBuilder.CreateMemberSalary(1, id, FrequencyIds.Month, monthlySalary);
            var member = MemberDataBuilder.CreateMember(id, memberAccounts, memberExpense, 
                memberSalary, name, emailAddress);

            // act
            var memberModel = _mapper.Map<MemberModel>(member);

            // assert
            memberModel.ShouldSatisfyAllConditions(
                () => memberModel.ShouldNotBeNull(),
                () => memberModel.Name.ShouldBe(member.Name),
                () => memberModel.Email.ShouldBe(member.Email),
                () => memberModel.Id.ShouldBe(member.Id),
                () => memberModel.MonthlySalary.ShouldBe(member.MemberSalary.GetMonthlySalary()),
                () => memberModel.MonthlyExpense.ShouldBe(member.MemberExpense.GetMonthlyExpense())
            );
        }


        [Fact]
        public void GivenAccountMapper_WhenMapListMemberRequestToListMemberQuery_ShouldMapSuccessful()
        {
            // assign
            var pagination = CommonRequestBuilder.CreateApiPaginationRequest(10, 3);

            var listMemberRequest = MemberRequestBuilder.CreateListMembersRequest("jack", pagination);
  
            // act
            var listMemberQuery = _mapper.Map<ListMembersQuery>(listMemberRequest);

            // assert
            listMemberQuery.ShouldSatisfyAllConditions(
                () => listMemberQuery.ShouldNotBeNull(),
                () => listMemberQuery.MemberName.ShouldBe(listMemberRequest.MemberName),
                () => listMemberQuery.Pagination.ShouldNotBeNull(),
                () => listMemberQuery.Pagination.PageSize.ShouldBe(listMemberRequest.Pagination.PageSize),
                () => listMemberQuery.Pagination.PageNumber.ShouldBe(listMemberRequest.Pagination.PageNumber));
        }

        [Theory]
        [InlineData("jack", "jack@zip.test.com", 1000, 3000)]
        [InlineData(null, null, 1000, 3000)]
        public void GivenAccountMapper_WhenMapSignUpMemberRequestToSignUpMemberCommand_ShouldMapSuccessful(
            string memberName, string email, decimal monthlySalary, decimal monthlyExpense)
        {
            // assign
            var signUpMemberRequest = MemberRequestBuilder.CreateSignUpMemberRequest(memberName, email, monthlySalary, monthlyExpense);

            // act
            var signUpMemberCommand = _mapper.Map<SignUpMemberCommand>(signUpMemberRequest);

            // assert
            signUpMemberCommand.ShouldSatisfyAllConditions(
                () => signUpMemberCommand.ShouldNotBeNull(),
                () => signUpMemberCommand.Name.ShouldBe(signUpMemberRequest.Name),
                () => signUpMemberCommand.Email.ShouldBe(signUpMemberRequest.Email),
                () => signUpMemberCommand.MonthlySalary.ShouldBe(signUpMemberRequest.MonthlySalary),
                () => signUpMemberCommand.MonthlyExpense.ShouldBe(signUpMemberRequest.MonthlyExpense));
        }
    }
}
