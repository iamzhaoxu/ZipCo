using System.ComponentModel.DataAnnotations;

namespace ZipCo.Users.WebApi.Requests
{
    public class SignUpMemberRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public decimal MonthlySalary { get; set; }

        public decimal MonthlyExpense { get; set; }
    }
}
