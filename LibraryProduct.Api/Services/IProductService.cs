using LibraryProduct.Api.DTOs;

namespace LibraryProduct.Api.Services
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(ProductCreateDto dto);
        Task<ProductDto> UpdateAsync(int id, ProductCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<ProductDto> GetByIdAsync(int id);
        Task<(IEnumerable<ProductDto> items, int total)> GetPagedAsync(string search, string category, int page, int pageSize, string sort);
    }
}
