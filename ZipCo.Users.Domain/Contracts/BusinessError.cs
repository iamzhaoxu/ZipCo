namespace ZipCo.Users.Domain.Contracts
{
    public class BusinessError
    {
        public string Message { get; }
        internal ErrorType ErrorType { get; }

        internal BusinessError(string message, ErrorType errorType)
        {
            Message = message;
            ErrorType = errorType;
        }
    }
}
