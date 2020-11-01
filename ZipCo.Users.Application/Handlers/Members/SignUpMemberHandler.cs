using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests.Members.Commands;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Interfaces;

namespace ZipCo.Users.Application.Handlers.Members
{
    public class SignUpMemberHandler : IRequestHandler<SignUpMemberCommand, SimpleResponse<Member>>
    {
        private readonly IMemberService _memberService;
        private readonly IMemberFactory _memberFactory;
        private readonly IUserUnitOfWork _userUnitOfWork;

        public SignUpMemberHandler(IMemberService memberService, 
            IMemberFactory memberFactory,
            IUserUnitOfWork userUnitOfWork)
        {
            _memberService = memberService;
            _memberFactory = memberFactory;
            _userUnitOfWork = userUnitOfWork;
        }

        public async Task<SimpleResponse<Member>> Handle(SignUpMemberCommand request, CancellationToken cancellationToken)
        {
            var member = _memberFactory.Create(request.Email, request.Name,
                request.MonthlyExpense, request.MonthlySalary);
            member = await _memberService.SignUpMember(member);
            await _userUnitOfWork.SaveChangesAsync(cancellationToken);
            member = await _memberService.GetMemberById(member.Id);
            return SimpleResponse<Member>.Create(member);
        }
    }
}
