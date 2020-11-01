using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.Infrastructure.Bootstrap.ErrorHandling;
using ZipCo.Users.Test.Shared;

namespace ZipCo.Users.Test.Unit.Infrastructure.Bootstrap.ErrorHandling
{
    public class GlobalExceptionHandlerTests
    {
        private readonly GlobalExceptionHandler _handler;
        private readonly RequestDelegate _requestDelegate;
        private readonly MockLogger<GlobalExceptionHandler> _logger;
        private readonly IHttpResponseHelper _httpResponseHelper;
        public GlobalExceptionHandlerTests()
        {
            _logger = Substitute.For<MockLogger<GlobalExceptionHandler>>();
            _requestDelegate = Substitute.For<RequestDelegate>();
            _httpResponseHelper = Substitute.For<IHttpResponseHelper>();
            _handler = new GlobalExceptionHandler(_requestDelegate, _logger, _httpResponseHelper);
        }

        [Fact]
        public async Task GivenGlobalExceptionHandler_WhenExceptionRaised_ShouldSendInternalServerError()
        {
            // assign
            var context = new DefaultHttpContext();
            var ex = new Exception("Some Exception");
            _requestDelegate.Invoke(Arg.Any<HttpContext>()).Returns(callInfo => throw ex);

            // act
            await _handler.Invoke(context);

            // assert
            AssertLogging(LogLevel.Critical, ex.Message, ex); 
            await AssertHttpResponse(context, HttpStatusCode.InternalServerError, "Unhandled Exception");
        }

        [Fact]
        public async Task GivenGlobalExceptionHandler_WhenBusinessExceptionRaised_IfErrorIsBadRequest_ShouldSendBadRequest()
        {
            // assign
            var context = new DefaultHttpContext();
            var parameters = new object[] { };
            var ex = new BusinessException("Some Exception", BusinessErrors.BadRequest("Bad Request"), parameters);
            _requestDelegate.Invoke(Arg.Any<HttpContext>()).Returns(callInfo => throw ex);

            // act
            await _handler.Invoke(context);

            // assert
            AssertLogging(LogLevel.Warning, ex.Message, null);
            await AssertHttpResponse(context, HttpStatusCode.BadRequest, ex.BusinessErrorMessage);
        }

        [Fact]
        public async Task GivenGlobalExceptionHandler_WhenBusinessExceptionRaised_IfErrorIsResourceNotFound_ShouldSendResourceNotFound()
        {
            // assign
            var context = new DefaultHttpContext();
            var parameters = new object[] { };
            var ex = new BusinessException("Some Exception", BusinessErrors.ResourceNotFound("Not Found",  "Not Found"), parameters);
            _requestDelegate.Invoke(Arg.Any<HttpContext>()).Returns(callInfo => throw ex);

            // act
            await _handler.Invoke(context);

            // assert
            AssertLogging(LogLevel.Warning, ex.Message, null);
            await AssertHttpResponse(context, HttpStatusCode.NotFound, ex.BusinessErrorMessage);
        }

        [Fact]
        public async Task GivenGlobalExceptionHandler_WhenBusinessExceptionRaised_IfErrorIsCritical_ShouldSendInternalServerError()
        {
            // assign
            var context = new DefaultHttpContext();
            var parameters = new object[] { };
            var ex = new BusinessException("Some Exception", BusinessErrors.Critical("Internal Server Error"), parameters);
            _requestDelegate.Invoke(Arg.Any<HttpContext>()).Returns(callInfo => throw ex);

            // act
            await _handler.Invoke(context);

            // assert
            AssertLogging(LogLevel.Error, ex.Message, ex);
            await AssertHttpResponse(context, HttpStatusCode.InternalServerError, ex.BusinessErrorMessage);
        }

        private async Task AssertHttpResponse(DefaultHttpContext context, HttpStatusCode httpStatusCode, string errorMessage)
        {
            await _httpResponseHelper.Received(1)
                .WriteJsonResponse(context,
                    Arg.Is<ErrorResponse>(e => AssertErrorResponse(e, httpStatusCode, errorMessage)));
            context.Response.StatusCode.ShouldBe((int) httpStatusCode);
        }

        private void AssertLogging(LogLevel logLevel, string errorMessage, Exception ex)
        {
            _logger.Received(1).MockLog(logLevel, errorMessage, ex);
        }

        private static bool AssertErrorResponse(ErrorResponse e, HttpStatusCode httpStatusCode, string errorDetails)
        {
            e.Status.ShouldBe((int)httpStatusCode);
            e.Errors.ShouldContain(errorDetails);
            return true;
        }
    }
}
