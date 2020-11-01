namespace ZipCo.Users.WebApi.Responses
{
    public class ApiSimpleResponse<T>
    {
        /// <summary>
        /// Response data
        /// </summary>
        public T Data { get; set; }
    }
}
