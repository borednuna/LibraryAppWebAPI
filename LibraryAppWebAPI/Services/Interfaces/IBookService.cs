namespace LibraryAppWebAPI.Services.Interfaces;

using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Enums;
using LibraryAppWebAPI.Models;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto?> GetBookByIdAsync(int id);
    Task<BookDto?> GetBookByIsbnAsync(string isbn);
    Task<BookDto> CreateBookAsync(BookDto book);
    Task<BookDto?> UpdateBookAsync(BookDto book);
    Task<bool> DeleteBookAsync(int id);
    Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(string author);
    Task<IEnumerable<BookDto>> GetBooksByGenreAsync(Genre genre);
    Task<IEnumerable<BookDto>> SearchBooksByTitleAsync(string title);
    // Task<bool> AssignBookToRackAsync(int bookId, int rackId);
    // Task<IEnumerable<BookDto>> GetBooksInRackAsync(int rackId);
}
