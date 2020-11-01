using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Interfaces;

namespace ZipCo.Users.Domain.Factories
{
    public class MemberFactory: IMemberFactory
    {
        public Member Create(string email, string name, decimal monthlyExpense, decimal monthlySalary)
        {
            return new Member
            {
                Email = email,
                Name = name,
                MemberExpense = new MemberExpense
                {
                    Amount = monthlyExpense,
                    BillFrequencyId = (long)FrequencyIds.Month
                },
                MemberSalary = new MemberSalary
                {
                    Amount = monthlySalary,
                    PayFrequencyId = (long)FrequencyIds.Month
                }
            };
        }
    }
}
