using LibraryProduct.Api.DTOs;

namespace LibraryProduct.Api.Services
{
    public interface ILibraryService
    {
        Task BorrowAsync(BorrowRequestDto dto);
        Task ReturnAsync(ReturnRequestDto dto);
    }
}
