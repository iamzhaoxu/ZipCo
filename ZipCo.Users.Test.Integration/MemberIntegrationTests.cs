using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Test.Shared.ApiRequestBuilders;
using ZipCo.Users.WebApi;
using ZipCo.Users.WebApi.Models;
using ZipCo.Users.WebApi.Requests;

namespace ZipCo.Users.Test.Integration
{
    /// <summary>
    /// We are not using InMemory Database for integration test since we have docker to raise the real DB
    /// for testing. However since the Hack Rank test execution does not support the sql server on docker,
    /// the integration tests need to be skip as default. 
    /// </summary>
    public class MemberIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public MemberIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        //[Fact(Skip = "Hack Rank Not Supported")]
        [Trait("Category", "Integration")]
        [Fact]
        public async Task GivenMemberController_WhenSignUpAMember_ShouldReturnMember()
        {
            using (var client = _factory.CreateClient())
            {
                // Assign
                var request = MemberRequestBuilder.CreateSignUpMemberRequest("jack", $"{Guid.NewGuid().ToString()}@gmail.com", 3000, 1000);

                // Act
                var httpResponse = await client.PostAsync(TestUrls.Member, JsonContent.Create(request));

                // Assert
                httpResponse.EnsureSuccessStatusCode(); // Status Code 200-299
                var data = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<SimpleResponse<MemberModel>>(data);
                response.ShouldNotBeNull();
                AssertMemberSameAsRequest(response.Data, request);
            }
        }

        //[Fact(Skip = "Hack Rank Not Supported")]
        [Trait("Category", "Integration")]
        [Fact]
        public async Task GivenMemberController_WhenListMembers_ShouldReturnMembers()
        {
            using (var client = _factory.CreateClient())
            {
                // assign
                var seed = Guid.NewGuid().ToString();
                var member1 = await client.SignUpMember("Jack" + seed + "1");
                var member2 = await client.SignUpMember("Jack" + seed + "2");
                await client.SignUpMember("Luck");
                var parameters = new Dictionary<string, string>
                {
                    {"MemberName", "Jack" + seed },
                    {"PageSize", "2"},
                    {"PageNumber", "1"},
                };

                // act
                var response = await client.ListMembers(parameters);

                // assert
                response.ShouldNotBeNull();
                var members = response.Data.ToList();
                AssertMemberExistedInList(members, member1);
                AssertMemberExistedInList(members, member2);
            }
        }

        //[Fact(Skip = "Hack Rank Not Supported")]
        [Trait("Category", "Integration")]
        [Fact]
        public async Task GivenMemberController_WhenGetMemberByEmail_ShouldReturnMember()
        {
            using (var client = _factory.CreateClient())
            {
                // assign
                var member = await client.SignUpMember("Jack");

                // act
                var actualMember = await client.GetMemberByEmail(member.Email);

                // assert
                AssertMember(actualMember, member);
            }
        }


        private static void AssertMemberSameAsRequest(MemberModel member, SignUpMemberRequest request)
        {
            member.ShouldNotBeNull();
            member.ShouldSatisfyAllConditions(
                () => member.Name.ShouldBe(request.Name),
                () => member.Email.ShouldBe(request.Email),
                () => member.MonthlyExpense.ShouldBe(request.MonthlyExpense),
                () => member.MonthlySalary.ShouldBe(request.MonthlySalary),
                () => member.Id.ShouldBeGreaterThan(0));
        }

        private static void AssertMember(MemberModel member, MemberModel expectedMember)
        {
            member.ShouldNotBeNull();
            member.ShouldSatisfyAllConditions(
                () => member.Name.ShouldBe(expectedMember.Name),
                () => member.Email.ShouldBe(expectedMember.Email),
                () => member.MonthlyExpense.ShouldBe(expectedMember.MonthlyExpense),
                () => member.MonthlySalary.ShouldBe(expectedMember.MonthlySalary),
                () => member.Id.ShouldBe(expectedMember.Id));
        }


        private static void AssertMemberExistedInList(List<MemberModel> members, MemberModel expectedMember)
        {
            var actualMember = members.FirstOrDefault(m => m.Email == expectedMember.Email);
            actualMember.ShouldNotBeNull();
            actualMember.Name.ShouldBe(expectedMember.Name);
            actualMember.MonthlySalary.ShouldBe(expectedMember.MonthlySalary);
            actualMember.MonthlyExpense.ShouldBe(expectedMember.MonthlyExpense);
        }
    }
}