using LibraryProduct.Api.Data;
using LibraryProduct.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LibraryProduct.Api.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ApplicationDbContext _db;
        private const decimal FinePerDay = 5m;

        public LibraryService(ApplicationDbContext db) { _db = db; }

        public async Task BorrowAsync(BorrowRequestDto dto)
        {
            var book = await _db.Books.FindAsync(dto.BookId) ?? throw new ArgumentException("Book not found");
            var borrower = await _db.Borrowers.FindAsync(dto.BorrowerId) ?? throw new ArgumentException("Borrower not found");

            if (book.Quantity <= 0) throw new InvalidOperationException("Book not available");

            book.Quantity -= 1;
            var record = new Models.BorrowRecord
            {
                BookId = book.Id,
                BorrowerId = borrower.Id,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(dto.Days),
                IsOverdue = false,
                FineAmount = 0
            };
            _db.BorrowRecords.Add(record);
            await _db.SaveChangesAsync();
        }

        public async Task ReturnAsync(ReturnRequestDto dto)
        {
            var record = await _db.BorrowRecords
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.BookId == dto.BookId && r.BorrowerId == dto.BorrowerId && r.ReturnDate == null);

            if (record == null) throw new ArgumentException("Active borrow record not found");

            record.ReturnDate = DateTime.UtcNow;
            if (record.ReturnDate > record.DueDate)
            {
                var daysLate = (record.ReturnDate.Value.Date - record.DueDate.Date).Days;
                record.IsOverdue = true;
                record.FineAmount = daysLate * FinePerDay;
            }
            record.Book.Quantity += 1;
            await _db.SaveChangesAsync();
        }
    }
}
