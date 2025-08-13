namespace LibraryAppWebAPI.Repositories
{
    using LibraryAppWebAPI.Data;
    using LibraryAppWebAPI.Models;
    using LibraryAppWebAPI.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                .Where(b => b.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            return book;
        }

        public async Task<Book?> GetByIsbnAsync(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<IEnumerable<Book>> SearchByTitleAsync(string title)
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchByAuthorAsync(string author)
        {
            return await _context.Books
                .Where(b => b.Author.Contains(author)).ToListAsync();
        }

        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateAsync(Book book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);
            if (existingBook == null)
                return null;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            existingBook.Genre = book.Genre;

            await _context.SaveChangesAsync();
            return existingBook;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            book.DeletedAt = new DateTime();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }
    }
}
