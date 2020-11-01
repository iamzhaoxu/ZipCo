using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ZipCo.Users.Application.Pipelines
{
    public class LogPipeLineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LogPipeLineBehavior<TRequest, TResponse>> _logger;

        public LogPipeLineBehavior(ILogger<LogPipeLineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName =  typeof(TRequest).Name;
            _logger.LogInformation($"Received request for {requestName}.");
            var response = next();
            _logger.LogInformation($"Returned response for {requestName}.");
            return response;
        }
    }
}
