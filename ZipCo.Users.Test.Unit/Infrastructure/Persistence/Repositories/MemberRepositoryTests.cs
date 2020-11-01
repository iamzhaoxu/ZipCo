using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Infrastructure.Persistence.DataContext;
using ZipCo.Users.Infrastructure.Persistence.Repositories;
using ZipCo.Users.Test.Shared.DataBuilders;
using ZipCo.Users.Test.Shared.InMemoryDb;

namespace ZipCo.Users.Test.Unit.Infrastructure.Persistence.Repositories
{
    public class MemberRepositoryTests
    {
        [Fact]
        public async Task GivenMemberRepository_WhenCallGetById_ShouldReturnMember()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                var member = await CreateMember(context);

                // act
                var repo = new MemberRepository(context);
                var actualMember = await repo.GetById(member.Id);

                // assert
                actualMember.ShouldNotBeNull();
                actualMember.ShouldSatisfyAllConditions(
                    () => actualMember.Id.ShouldBe(member.Id),
                    () => actualMember.MemberAccounts.Count.ShouldBe(0),
                    () => actualMember.MemberExpense.ShouldNotBeNull(),
                    () => actualMember.MemberSalary.ShouldNotBeNull());
            }
        }

        [Fact]
        public async Task GivenMemberRepository_WhenCallGetByEmail_ShouldReturnMember()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                var member = await CreateMember( context);

                // act
                var repo = new MemberRepository(context);
                var actualMember = await repo.GetByEmail(member.Email);

                // assert
                actualMember.ShouldNotBeNull();
                actualMember.ShouldSatisfyAllConditions(
                    () => actualMember.Id.ShouldBe(member.Id),
                    () => actualMember.MemberAccounts.Count.ShouldBe(0),
                    () => actualMember.MemberExpense.ShouldNotBeNull(),
                    () => actualMember.MemberSalary.ShouldNotBeNull());
            }
        }

        [Fact]
        public async Task GivenMemberRepository_WhenCallCreate_ShouldSuccess()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                var member = MemberDataBuilder.CreateMember(1, new List<MemberAccount>(), null, null);

                // act
                var repo = new MemberRepository(context);
                await repo.Create(member);
                await context.SaveChangesAsync();

                // assert
                member.Id.ShouldBeGreaterThan(0);
            }
        }

        [Fact] public async Task GivenMemberRepository_WhenCallListAll_ShouldSuccess()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                var member1 = MemberDataBuilder.CreateMember(1, new List<MemberAccount>(), null, null, "Rom", "tom@zip.com");
                var member2 = MemberDataBuilder.CreateMember(2, new List<MemberAccount>(), null, null, "Shawn", "shawn@zip.com");
                var member3 = MemberDataBuilder.CreateMember(3, new List<MemberAccount>(), null, null, "Owen", "owen@zip.com");

                await context.AddRangeAsync(new[] {member1, member2, member3});
                await context.SaveChangesAsync();

                // act
                var repo = new MemberRepository(context);
                var members = await repo.ListAll(new PaginationRequest{PageSize = 1, PageNumber = 1}, "n");

                // assert
                members.ShouldNotBeNull();
                members.TotalPageNumber.ShouldBe(2);
                members.Data.ShouldContain(m => m.Id == member3.Id);

                // act
                members = await repo.ListAll(new PaginationRequest { PageSize = 1, PageNumber = 2 }, "n");

                // assert
                members.ShouldNotBeNull();
                members.TotalPageNumber.ShouldBe(2);
                members.Data.ShouldContain(m => m.Id == member2.Id);

                // act
                members = await repo.ListAll(new PaginationRequest { PageSize = 2, PageNumber = 1 }, "n");

                // assert
                members.ShouldNotBeNull();
                members.TotalPageNumber.ShouldBe(1);
                members.Data.ShouldContain(m => m.Id == member2.Id);
                members.Data.ShouldContain(m => m.Id == member3.Id);
            }
        }

        private static async Task<Member> CreateMember(UserContext context)
        {
            var member = MemberDataBuilder.CreateMember(1, new List<MemberAccount>(),null, null);
            await context.Members.AddAsync(member);
            await context.SaveChangesAsync();
            return member;
        }
    }
}
