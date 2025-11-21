namespace LibraryProduct.Api.Models
{
    public class ExternalApiLog
    {
        public int Id { get; set; }
        public string? Endpoint { get; set; }
        public string? RequestData { get; set; }
        public string? ResponseData { get; set; }
        public DateTime CalledAt { get; set; }
        public int StatusCode { get; set; }
    }
}
