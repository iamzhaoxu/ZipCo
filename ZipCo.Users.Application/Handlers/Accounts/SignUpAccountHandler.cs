using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests.Accounts.Commands;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.Domain.Entities.Accounts;

namespace ZipCo.Users.Application.Handlers.Accounts
{
    public class SignUpAccountHandler: IRequestHandler<SignUpAccountCommand, SimpleResponse<Account>>
    {
        private readonly IMemberService _memberService;
        private readonly IAccountService _accountService;
        private readonly IUserUnitOfWork _userUnitOfWork;

        public SignUpAccountHandler(IMemberService memberService, 
            IAccountService accountService,
            IUserUnitOfWork userUnitOfWork)

        {
            _memberService = memberService;
            _accountService = accountService;
            _userUnitOfWork = userUnitOfWork;
        }

        public async Task<SimpleResponse<Account>> Handle(SignUpAccountCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberService.GetMemberById(request.MemberId);
            if (member == null)
            {
                throw new BusinessException(
                            "Member {memberId} is invalid.",
                            BusinessErrors.BadRequest("Invalid member details."),
                            request.MemberId);

            }
            var account = await _accountService.SignUpAccount(member);
            await _userUnitOfWork.SaveChangesAsync(cancellationToken);
            account = await _accountService.GetAccountById(account.Id);
            return SimpleResponse<Account>.Create(account);
        }
    }
}
