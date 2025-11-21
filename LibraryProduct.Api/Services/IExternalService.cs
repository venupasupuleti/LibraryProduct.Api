namespace LibraryProduct.Api.Services
{
    public interface IExternalService
    {
        Task<string> GetBookInfoAsync(string isbn);
        Task LogCallAsync(string endpoint, string request, string response, int statusCode);
        Task<IEnumerable<object>> GetLogsAsync(int page, int pageSize);
    }
}
