namespace ZipCo.Users.Application.Responses
{
    public class SimpleResponse<T>
    {
        public T Data { get; set; }

        public SimpleResponse() { }

        private SimpleResponse(T data)
        {
            Data = data;
        }

        public static SimpleResponse<TR> Create<TR>(TR data)
        {
            return new SimpleResponse<TR>(data);
        }
    }
}
