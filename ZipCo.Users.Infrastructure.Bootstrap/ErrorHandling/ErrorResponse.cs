namespace ZipCo.Users.Infrastructure.Bootstrap.ErrorHandling
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string[] Errors { get; set; } = { };
    }
}
