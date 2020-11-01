using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ZipCo.Users.Domain.Contracts;

namespace ZipCo.Users.Application.Pipelines
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(v => v.Errors)
                .Where(e => e != null)
                .Select(e => e.ErrorMessage)
                .ToList();
            if (errors.Any())
            {
                var builder = new StringBuilder();
                errors.ForEach(e => builder.AppendLine(e));
                var errorMessage = builder.ToString();
                throw new BusinessException(errorMessage, BusinessErrors.BadRequest(errorMessage));
            }
            return next();
        }
    }
}
