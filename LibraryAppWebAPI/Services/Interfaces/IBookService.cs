namespace LibraryAppWebAPI.Services.Interfaces;

using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Enums;
using LibraryAppWebAPI.Models;

public interface IBookService
{
    Task<IEnumerable<BookDtoResponse>> GetAllBooksAsync();
    Task<BookDtoResponse?> GetBookByIdAsync(int id);
    Task<BookDtoResponse?> GetBookByIsbnAsync(string isbn);
    Task<BookDtoResponse> CreateBookAsync(BookDtoRequest book);
    Task<BookDtoResponse?> UpdateBookAsync(BookDtoRequest book);
    Task<bool> DeleteBookAsync(int id);
}
