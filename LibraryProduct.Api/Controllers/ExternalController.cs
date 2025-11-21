using LibraryProduct.Api.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryProduct.Api.Controllers
{
    [ApiController]
    [Route("api/external")]
    public class ExternalController : ControllerBase
    {
        private readonly IExternalService _svc;
        public ExternalController(IExternalService svc) => _svc = svc;

        [HttpGet("bookinfo/{isbn}")]
        public async Task<IActionResult> BookInfo(string isbn)
        {
            var result = await _svc.GetBookInfoAsync(isbn);
            return Ok(new { data = result });
        }

        [HttpGet("logs")]
        public async Task<IActionResult> Logs([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var logs = await _svc.GetLogsAsync(page, pageSize);
            return Ok(logs);
        }
    }
}
