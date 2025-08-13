namespace LibraryAppWebAPI.Services.Interfaces;

using LibraryAppWebAPI.Enums;
using LibraryAppWebAPI.Models;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book?> GetBookByIsbnAsync(string isbn);
    Task<Book> CreateBookAsync(Book book);
    Task<Book?> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id);
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
    Task<IEnumerable<Book>> GetBooksByGenreAsync(Genre genre);
    Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title);
    // Task<bool> AssignBookToRackAsync(int bookId, int rackId);
    // Task<IEnumerable<Book>> GetBooksInRackAsync(int rackId);
}
