namespace WebApiCore8Sample.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Status { get; set; } = true;
        public int StatusCode { get; set; }
        public string Detail { get; set; } = "";
    }
}
