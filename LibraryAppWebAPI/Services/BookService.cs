namespace LibraryAppWebAPI.Services;

using LibraryAppWebAPI.Enums;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Repositories;
using LibraryAppWebAPI.Repositories.Interfaces;
using LibraryAppWebAPI.Services.Interfaces;

public class BookService(IBookRepository bookRepository, IRackRepository rackRepository) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IRackRepository _rackRepository = rackRepository;

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return book;
    }

    public async Task<Book?> GetBookByIsbnAsync(string isbn)
    {
        var books = await _bookRepository.GetAllAsync();
        return books.FirstOrDefault(b => b.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        var existing = await GetBookByIsbnAsync(book.ISBN);
        if (existing != null)
            throw new InvalidOperationException($"A book with ISBN {book.ISBN} already exists.");

        return await _bookRepository.AddAsync(book);
    }

    public async Task<Book?> UpdateBookAsync(Book book)
    {
        var existing = await GetBookByIsbnAsync(book.ISBN);
        if (existing != null && existing.Id != book.Id)
            throw new InvalidOperationException($"Another book with ISBN {book.ISBN} already exists.");

        return await _bookRepository.UpdateAsync(book);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        return await _bookRepository.DeleteAsync(id);
    }

    // Searching & filtering
    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author)
    {
        var books = await _bookRepository.GetAllAsync();
        return books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<Book>> GetBooksByGenreAsync(Genre genre)
    {
        var books = await _bookRepository.GetAllAsync();
        return books.Where(b => b.Genre == genre);
    }

    public async Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title)
    {
        var books = await _bookRepository.GetAllAsync();
        return books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
    }

    // public async Task<bool> AssignBookToRackAsync(int bookId, int rackId)
    // {
    //     var rack = await _rackRepository.GetByIdAsync(rackId);
    //     if (rack == null)
    //         throw new InvalidOperationException("Rack not found.");

    //     var booksInRack = await _bookRepository.GetBooksInRackAsync(rackId);
    //     if (booksInRack.Count() >= rack.Capacity)
    //         throw new InvalidOperationException("Rack is already at full capacity.");

    //     var book = await _bookRepository.GetByIdAsync(bookId);
    //     if (book == null)
    //         throw new InvalidOperationException("Book not found.");

    //     book.RackId = rackId;
    //     await _bookRepository.UpdateAsync(book);
    //     return true;
    // }

    // public async Task<IEnumerable<Book>> GetBooksInRackAsync(int rackId)
    // {
    //     return await _bookRepository.GetBooksInRackAsync(rackId);
    // }
}
