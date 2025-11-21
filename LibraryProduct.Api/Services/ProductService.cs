using LibraryProduct.Api.Data;
using LibraryProduct.Api.DTOs;
using LibraryProduct.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProduct.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext db) { _db = db; }

        public async Task<ProductDto> CreateAsync(ProductCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.SKU))
                throw new ArgumentException("SKU required");

            var exists = await _db.Products.AnyAsync(p => p.SKU == dto.SKU);
            if (exists) throw new InvalidOperationException("SKU must be unique");

            var p = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                SKU = dto.SKU,
                Category = dto.Category,
                Price = dto.Price,
                QuantityInStock = dto.QuantityInStock,
                Manufacturer = dto.Manufacturer,
                Weight = dto.Weight,
                Dimensions = dto.Dimensions
            };
            _db.Products.Add(p);
            await _db.SaveChangesAsync();
            return Map(p);
        }

        public async Task<ProductDto> UpdateAsync(int id, ProductCreateDto dto)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return null;
            if (!string.Equals(p.SKU, dto.SKU, StringComparison.OrdinalIgnoreCase))
            {
                var exists = await _db.Products.AnyAsync(x => x.SKU == dto.SKU && x.ProductId != id);
                if (exists) throw new InvalidOperationException("SKU must be unique");
            }
            p.Name = dto.Name;
            p.Description = dto.Description;
            p.SKU = dto.SKU;
            p.Category = dto.Category;
            p.Price = dto.Price;
            p.QuantityInStock = dto.QuantityInStock;
            p.Manufacturer = dto.Manufacturer;
            p.Weight = dto.Weight;
            p.Dimensions = dto.Dimensions;

            await _db.SaveChangesAsync();
            return Map(p);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return false;
            _db.Products.Remove(p);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var p = await _db.Products.FindAsync(id);
            return p == null ? null : Map(p);
        }

        public async Task<(IEnumerable<ProductDto> items, int total)> GetPagedAsync(string search, string category, int page, int pageSize, string sort)
        {
            var q = _db.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) q = q.Where(x => x.Name.Contains(search) || x.Description.Contains(search) || x.SKU.Contains(search));
            if (!string.IsNullOrWhiteSpace(category)) q = q.Where(x => x.Category == category);

            var total = await q.CountAsync();

            // simple sort
            q = sort switch
            {
                "price_asc" => q.OrderBy(x => x.Price),
                "price_desc" => q.OrderByDescending(x => x.Price),
                "name_desc" => q.OrderByDescending(x => x.Name),
                _ => q.OrderBy(x => x.Name)
            };

            var items = await q.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new ProductDto
                {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    SKU = x.SKU,
                    Price = x.Price,
                    QuantityInStock = x.QuantityInStock,
                    Category = x.Category
                }).ToListAsync();

            return (items, total);
        }

        private static ProductDto Map(Product? p)
        {
            if (p == null) return null!; // or throw
            return new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                SKU = p.SKU,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock,
                Category = p.Category
            };
        }

    }
}
