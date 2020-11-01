using MediatR;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Application.Requests.Members.Commands
{
    public class SignUpMemberCommand : IRequest<SimpleResponse<Member>>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public decimal MonthlySalary { get; set; }

        public decimal MonthlyExpense { get; set; }

    }
}
