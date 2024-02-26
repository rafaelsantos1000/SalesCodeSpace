namespace SalesCodeSpace.Responses
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = null!;

        public object Result { get; set; } = null!;
    }
}


