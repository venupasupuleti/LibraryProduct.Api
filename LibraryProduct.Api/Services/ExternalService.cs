using LibraryProduct.Api.Data;
using LibraryProduct.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using SysJson = System.Text.Json;

namespace LibraryProduct.Api.Services
{
    public class ExternalService : IExternalService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _cfg;

        public ExternalService(ApplicationDbContext db, IMemoryCache cache, IConfiguration cfg)
        {
            _db = db; _cache = cache; _cfg = cfg;
        }

        public async Task<string> GetBookInfoAsync(string isbn)
        {
            var key = $"bookinfo:{isbn}";
            if (_cache.TryGetValue(key, out string cached)) return cached;

            var mock = new
            {
                isbn = isbn,
                title = $"Sample Book for {isbn}",
                author = "Author Name",
                published = "2020-01-01",
                description = "This is a mocked external book info."
            };

            var raw = SysJson.JsonSerializer.Serialize(mock);

            var info = new ExternalBookInfo { ISBN = isbn, RawJson = raw, RetrievedAt = DateTime.UtcNow };
            _db.ExternalBookInfos.Add(info);
            _db.ExternalApiLogs.Add(new ExternalApiLog
            {
                Endpoint = $"/external/bookinfo/{isbn}",
                RequestData = isbn,
                ResponseData = raw,
                CalledAt = DateTime.UtcNow,
                StatusCode = 200
            });
            await _db.SaveChangesAsync();

            var ttl = _cfg.GetValue<int>("ExternalApi:CacheTtlMinutes", 10);
            _cache.Set(key, raw, TimeSpan.FromMinutes(ttl));
            return raw;
        }

        public async Task LogCallAsync(string endpoint, string request, string response, int statusCode)
        {
            _db.ExternalApiLogs.Add(new ExternalApiLog
            {
                Endpoint = endpoint,
                RequestData = request,
                ResponseData = response,
                CalledAt = DateTime.UtcNow,
                StatusCode = statusCode
            });
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetLogsAsync(int page, int pageSize)
        {
            var q = _db.ExternalApiLogs.OrderByDescending(l => l.CalledAt)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(l => new { l.Id, l.Endpoint, l.RequestData, l.ResponseData, l.CalledAt, l.StatusCode });
            return await q.ToListAsync();
        }
    }
}
