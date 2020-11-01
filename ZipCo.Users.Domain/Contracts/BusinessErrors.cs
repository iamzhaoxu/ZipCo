namespace ZipCo.Users.Domain.Contracts
{
    public static class BusinessErrors
    {
        public static BusinessError ResourceNotFound(string resource, string details)
        {
            var message = $"Could not find {resource} resource. {details}.";
            return new BusinessError(message, ErrorType.ResourceNotFound);
        }

        public static BusinessError BadRequest(string details)
        {
            var message = $"Request is invalid. {details}";
            return new BusinessError(message, ErrorType.BadRequest);
        }

        public static BusinessError Critical(string details)
        {
            var message = $"An unexpected error raised. {details}";
            return new BusinessError(message, ErrorType.Critical);
        }
    }
}
