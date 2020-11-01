using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NSubstitute;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Pipelines;
using ZipCo.Users.Application.Requests.Accounts.Commands;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Application.Validators;
using ZipCo.Users.Application.Validators.Accounts;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.WebApi.Models;

namespace ZipCo.Users.Test.Unit.Core.Application.Pipelines
{
    public class ValidationPipelineBehaviorTests
    {
        private readonly ValidationPipelineBehavior<SignUpAccountCommand, 
            SimpleResponse<AccountModel>> _pipeline;

        public ValidationPipelineBehaviorTests()
        {
            var validator = new SignUpAccountValidator();
            _pipeline = new ValidationPipelineBehavior<SignUpAccountCommand,
                SimpleResponse<AccountModel>>(new []{ validator });
        }

        [Fact]
        public async Task GivenValidationPipelineBehavior_WhenCallHandle_ShouldValidateRequest()
        {
            // assign
            var command = new SignUpAccountCommand
            {
                MemberId = 0
            };
            var next = Substitute.For<RequestHandlerDelegate<SimpleResponse<AccountModel>>>();

            // act
            var ex =  Should.Throw<BusinessException>(() => _pipeline.Handle(command, CancellationToken.None, next));

            // assert
            ex.IsBadRequest.ShouldBeTrue();
            ex.BusinessErrorMessage.ShouldContain(ValidationTokens.InvalidMemberId);
            await next.DidNotReceive().Invoke();
        }
    }
}
