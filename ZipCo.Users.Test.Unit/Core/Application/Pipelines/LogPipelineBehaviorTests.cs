using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Pipelines;
using ZipCo.Users.Application.Requests.Accounts.Commands;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Application.Validators;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.Test.Shared;
using ZipCo.Users.WebApi.Models;

namespace ZipCo.Users.Test.Unit.Core.Application.Pipelines
{
    public class LogPipelineBehaviorTests
    {
        private readonly LogPipeLineBehavior<SignUpAccountCommand, 
            SimpleResponse<AccountModel>> _pipeline;

        private readonly MockLogger<LogPipeLineBehavior<SignUpAccountCommand,
            SimpleResponse<AccountModel>>> _logger;
        public LogPipelineBehaviorTests()
        {
            _logger = Substitute.For<MockLogger<LogPipeLineBehavior<SignUpAccountCommand,
                SimpleResponse<AccountModel>>>>();
            _pipeline = new LogPipeLineBehavior<SignUpAccountCommand,
                SimpleResponse<AccountModel>>(_logger);
        }

        [Fact]
        public void GivenLogPipelineBehavior_WhenCallHandle_ShouldLogRequestResponse()
        {
            // assign
            var command = new SignUpAccountCommand
            {
                MemberId = 0
            };
            var next = Substitute.For<RequestHandlerDelegate<SimpleResponse<AccountModel>>>();
            next.Invoke().Returns(new SimpleResponse<AccountModel>());
            
            // act
             _pipeline.Handle(command, CancellationToken.None, next);

            // assert
            _logger.Received(1).MockLog(LogLevel.Information, "Received request for SignUpAccountCommand.", null);
            _logger.Received(1).MockLog(LogLevel.Information, "Returned response for SignUpAccountCommand.", null);
        }
    }
}
