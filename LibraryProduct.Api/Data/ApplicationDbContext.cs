using LibraryProduct.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace LibraryProduct.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<ExternalApiLog> ExternalApiLogs { get; set; }
        public DbSet<ExternalBookInfo> ExternalBookInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Product>().HasIndex(p => p.SKU).IsUnique();
            mb.Entity<Borrower>().HasIndex(b => b.MembershipId).IsUnique();
            mb.Entity<Book>().HasIndex(b => b.ISBN);
            mb.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            mb.Entity<BorrowRecord>().Property(r => r.FineAmount).HasPrecision(18, 2);

            base.OnModelCreating(mb);
        }
    }
}
