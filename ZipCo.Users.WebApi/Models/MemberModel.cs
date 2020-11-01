namespace ZipCo.Users.WebApi.Models
{
    public class MemberModel
    {
        public  long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public decimal MonthlySalary { get; set; }

        public decimal MonthlyExpense { get; set; }

    }
}
