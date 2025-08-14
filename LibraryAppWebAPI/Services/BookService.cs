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

        public async Task<IEnumerable<BookDtoResponse>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDtoResponse>>(books);
        }

        public async Task<BookDtoResponse?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDtoResponse>(book);
        }

        public async Task<BookDtoResponse?> GetBookByIsbnAsync(string isbn)
        {
            var books = await _bookRepository.GetAllAsync();
            var book = books.FirstOrDefault(b => b.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
            return book == null ? null : _mapper.Map<BookDtoResponse>(book);
        }

        public async Task<BookDtoResponse> CreateBookAsync(BookDtoRequest bookDto)
        {
            var existing = await GetBookByIsbnAsync(bookDto.ISBN);
            if (existing != null)
                throw new InvalidOperationException($"A book with ISBN {bookDto.ISBN} already exists.");

            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.AddAsync(book);

            return _mapper.Map<BookDtoResponse>(book); 
        }

        public async Task<BookDtoResponse?> UpdateBookAsync(BookDtoRequest bookDto)
        {
            if (bookDto.Id == null)
            {
                return null;
            }
            var existing = await _bookRepository.GetByIdAsync((int)bookDto.Id);
            if (existing == null) return null;

            _mapper.Map(bookDto, existing);
            await _bookRepository.UpdateAsync(existing);

            return _mapper.Map<BookDtoResponse>(existing);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }
    }
}
