using LibraryProduct.Api.DTOs;
using LibraryProduct.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProduct.Api.Controllers
{
    [ApiController]
    [Route("api/library")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _svc;
        public LibraryController(ILibraryService svc) => _svc = svc;

        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow([FromBody] BorrowRequestDto dto)
        {
            try { await _svc.BorrowAsync(dto); return Ok(new { message = "Borrowed" }); }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }

        [HttpPost("return")]
        public async Task<IActionResult> Return([FromBody] ReturnRequestDto dto)
        {
            try { await _svc.ReturnAsync(dto); return Ok(new { message = "Returned" }); }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }
    }
}
