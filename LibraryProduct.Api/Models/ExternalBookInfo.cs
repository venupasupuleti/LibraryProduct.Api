namespace LibraryProduct.Api.Models
{
    public class ExternalBookInfo
    {
        public int Id { get; set; }
        public string? ISBN { get; set; }
        public string? RawJson { get; set; }
        public DateTime RetrievedAt { get; set; }
    }
}
