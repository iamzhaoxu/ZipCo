using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Domain.Interfaces
{
    public interface IMemberFactory
    {
        Member Create(string email, string name, decimal monthlyExpense, decimal monthlySalary);
    }
}