using System;

namespace ZipCo.Users.Domain.Contracts
{
    public class BusinessException : Exception
    {
        private readonly BusinessError _businessError;
        public object[] Parameters { get; }

        public bool IsBadRequest => _businessError.ErrorType == ErrorType.BadRequest;
        public bool IsResourceNotFound => _businessError.ErrorType == ErrorType.ResourceNotFound;
        public bool IsCritical => _businessError.ErrorType == ErrorType.Critical;

        public string BusinessErrorMessage => _businessError.Message;
        public BusinessException(string message, BusinessError businessError, params object[] parameters) : base(message)
        {
            _businessError = businessError;
            Parameters = parameters;
        }
    }
}
