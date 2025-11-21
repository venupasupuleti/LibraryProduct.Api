using LibraryProduct.Api.DTOs;
using LibraryProduct.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProduct.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _svc;
        public ProductsController(IProductService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
        {
            try
            {
                var p = await _svc.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = p.ProductId }, p);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _svc.GetByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string search, [FromQuery] string category, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string sort = null)
        {
            var (items, total) = await _svc.GetPagedAsync(search, category, page, pageSize, sort);
            return Ok(new { items, page, pageSize, total });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateDto dto)
        {
            try
            {
                var p = await _svc.UpdateAsync(id, dto);
                if (p == null) return NotFound();
                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
