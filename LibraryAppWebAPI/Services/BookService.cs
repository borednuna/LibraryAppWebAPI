using AutoMapper;
using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Enums;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Repositories.Interfaces;
using LibraryAppWebAPI.Services.Interfaces;

namespace LibraryAppWebAPI.Services
{
    public class BookService(IBookRepository bookRepository, IMapper mapper) : IBookService
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto?> GetBookByIsbnAsync(string isbn)
        {
            var books = await _bookRepository.GetAllAsync();
            var book = books.FirstOrDefault(b => b.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateBookAsync(BookDto bookDto)
        {
            var existing = await GetBookByIsbnAsync(bookDto.ISBN);
            if (existing != null)
                throw new InvalidOperationException($"A book with ISBN {bookDto.ISBN} already exists.");

            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.AddAsync(book);

            return _mapper.Map<BookDto>(book); 
        }

        public async Task<BookDto?> UpdateBookAsync(BookDto bookDto)
        {
            if (bookDto.Id == null)
            {
                return null;
            }
            var existing = await _bookRepository.GetByIdAsync((int)bookDto.Id);
            if (existing == null) return null;

            _mapper.Map(bookDto, existing);
            await _bookRepository.UpdateAsync(existing);

            return _mapper.Map<BookDto>(existing);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(string author)
        {
            var books = await _bookRepository.GetAllAsync();
            var filtered = books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
            return _mapper.Map<IEnumerable<BookDto>>(filtered);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByGenreAsync(Genre genre)
        {
            var books = await _bookRepository.GetAllAsync();
            var filtered = books.Where(b => b.Genre == genre);
            return _mapper.Map<IEnumerable<BookDto>>(filtered);
        }

        public async Task<IEnumerable<BookDto>> SearchBooksByTitleAsync(string title)
        {
            var books = await _bookRepository.GetAllAsync();
            var filtered = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            return _mapper.Map<IEnumerable<BookDto>>(filtered);
        }
    }
}
