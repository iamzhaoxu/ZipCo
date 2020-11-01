using System.Collections.Generic;

namespace ZipCo.Users.Domain.Entities.Members
{
    public class Member : AudibleBaseEntity
    { 
        public string Name { get; set; }

        public string Email { get; set; }

        public MemberSalary MemberSalary { get; set; }

        public MemberExpense MemberExpense { get; set; }

        public ICollection<MemberAccount> MemberAccounts { get; set; }

        public decimal GetMonthNetIncome()
        {
            return MemberSalary.GetMonthlySalary() - MemberExpense.GetMonthlyExpense();
        }
    }
}
