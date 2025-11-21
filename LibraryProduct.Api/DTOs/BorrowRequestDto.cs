namespace LibraryProduct.Api.DTOs
{
    public class BorrowRequestDto
    {
        public int BorrowerId { get; set; }
        public int BookId { get; set; }
        public int Days { get; set; }
    }
}
