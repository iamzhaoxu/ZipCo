﻿using FluentValidation;
using ZipCo.Users.Application.Requests;

namespace ZipCo.Users.Application.Validators
{
    public static class ValidatorExtension
    {
        public static IRuleBuilderOptions<T, PaginationRequest> PaginationRequestValid<T>(this IRuleBuilderInitial<T, PaginationRequest> builder)
        {
            return builder.IsRequired(ValidationTokens.Pagination)
                .Must(p => p.PageSize > 0)
                .WithMessage(ValidationTokens.PageSizeTooSmall)
                .Must(p => p.PageNumber > 0)
                .WithMessage(ValidationTokens.PageNumberTooSmall)
                .Must(p => p.PageSize < 1000)
                .WithMessage(p => ValidationTokens.PageSizeTooLarge)
                .Must(p => p.PageNumber < 10000)
                .WithMessage(p => ValidationTokens.PageNumberTooLarge);
        }

        public static IRuleBuilderOptions<T, TProperty> IsRequired<T, TProperty>(this IRuleBuilderInitial<T, TProperty> builder, string name)
        {
            return builder
                .NotEmpty()
                .WithMessage(string.Format(ValidationTokens.IsRequire, name));
        }

        public static IRuleBuilderOptions<T, string> EmailRequired<T>(this IRuleBuilderInitial<T, string> builder, string name)
        {
            return builder
                .IsRequired(name)
                .EmailAddress()
                .WithMessage(string.Format(ValidationTokens.InvalidEmailFormat, name));
        }
    }
}
