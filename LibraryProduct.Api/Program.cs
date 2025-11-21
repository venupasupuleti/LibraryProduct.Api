using LibraryProduct.Api.Data;
using LibraryProduct.Api.Services;
using Microsoft.EntityFrameworkCore;
namespace LibraryProduct.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Caching
            builder.Services.AddMemoryCache();

            // Register services
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ILibraryService, LibraryService>();
            builder.Services.AddScoped<IExternalService, ExternalService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
