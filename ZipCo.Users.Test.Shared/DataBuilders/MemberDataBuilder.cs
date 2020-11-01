using System;
using System.Collections.Generic;
using System.Linq;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Test.Shared.DataBuilders
{
    public static class MemberDataBuilder
    {

        public static Member CreateMember(long id,
            IEnumerable<MemberAccount> memberAccounts,
            MemberExpense memberExpense,
            MemberSalary memberSalary,
            string name = "jack",
            string email = null
        )
        {

            email ??= $"{Guid.NewGuid()}@zip.test.com";
            memberAccounts ??= new MemberAccount[] { };
            memberExpense ??= CreateMemberExpense(id + 10, id, FrequencyIds.Month); 
            memberSalary ??= CreateMemberSalary(id + 100, id, FrequencyIds.Month);
            return new Member
            {
                Id = id,
                MemberAccounts = memberAccounts.ToArray(),
                MemberExpense = memberExpense,
                MemberSalary = memberSalary,
                ModifiedOn = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow,
                Name = name,
                Email = email
            };
        }

        public static MemberSalary CreateMemberSalary(long memberSalaryId, long memberId,
            FrequencyIds frequencyId = FrequencyIds.Month, decimal amount = 3000)
        {
            return new MemberSalary
            {
                Id = memberSalaryId,
                MemberId = memberId,
                Amount = amount,
                PayFrequencyId = (long)frequencyId,
                ModifiedOn = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow

            };
        }

        public static MemberExpense CreateMemberExpense(long memberExpenseId, long memberId,
            FrequencyIds frequencyId = FrequencyIds.Month, decimal amount = 1000)

        {
            return new MemberExpense
            {
                Id = memberExpenseId,
                MemberId = memberId,
                Amount = amount,
                BillFrequencyId = (long)frequencyId,
                ModifiedOn = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow

            };
        }


        public static MemberAccount CreateMemberAccount(long id, long accountId, long memberId)
        {
            return new MemberAccount
            {
                Id = id,
                AccountId = accountId,
                MemberId = memberId
            };
        }

    }
}
