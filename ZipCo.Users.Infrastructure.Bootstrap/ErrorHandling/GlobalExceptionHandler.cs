using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ZipCo.Users.Domain.Contracts;
using ZIpCo.Utility.Formatting.Extensions;

namespace ZipCo.Users.Infrastructure.Bootstrap.ErrorHandling
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _nextHandler;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHttpResponseHelper _httpResponseHelper;

        public GlobalExceptionHandler(RequestDelegate nextHandler, 
            ILogger<GlobalExceptionHandler> logger,
            IHttpResponseHelper httpResponseHelper)
        {
            _logger = logger;
            _httpResponseHelper = httpResponseHelper;
            _nextHandler = nextHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _nextHandler(context);
            }
            catch (BusinessException ex)
            {
                await HandleBusinessException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleException(HttpStatusCode.InternalServerError, context, ex);
            }
        }

        private async Task HandleBusinessException(HttpContext context, BusinessException ex)
        {
            var statusCode = MapHttpStatusCode(ex);
            LogByHttpStatusCode(ex, statusCode);
            await SendFailedResponse(ex.BusinessErrorMessage, statusCode, context);
        }

        private async Task HandleException(HttpStatusCode statusCode, HttpContext context, Exception ex)
        {
            foreach (var exception in ex.FlatException())
            {
                _logger.LogCritical(exception, exception.Message);
            }
            await SendFailedResponse("Unhandled Exception", statusCode, context);
        }

        private void LogByHttpStatusCode(BusinessException ex, HttpStatusCode statusCode)
        {
            var statusCodeInt = (int) statusCode;
            if (statusCodeInt >= 400 && statusCodeInt < 500)
            {
                _logger.LogWarning(ex.Message, ex.Parameters);
            }
            else if (statusCodeInt >= 500)
            {
                _logger.LogError(ex, ex.Message, ex.Parameters);
            }
        }

        private HttpStatusCode MapHttpStatusCode(BusinessException ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            if (ex.IsBadRequest)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (ex.IsResourceNotFound)
            {
                statusCode = HttpStatusCode.NotFound;
            } 
            else if (ex.IsCritical)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            return statusCode;
        }

        private async Task SendFailedResponse(string errorDetails, HttpStatusCode statusCode, HttpContext context)
        {
            var result = new ErrorResponse
            {
                Status = (int)statusCode,
                Errors = new []
                {
                    errorDetails
                }
            };
            context.Response.StatusCode = (int) statusCode;
            await _httpResponseHelper.WriteJsonResponse(context, result);
        }
    }
}
