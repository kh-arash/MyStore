namespace MyStore.Service.Models
{
    public class ResponseMessage<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
