using FluentValidation;
using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService bookService, IValidator<BookDto> bookValidator) : ControllerBase
{
    private readonly IBookService _bookService = bookService;
    private readonly IValidator<BookDto> _bookValidator = bookValidator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
    {
        try
        {
            var books = await _bookService.GetAllBooksAsync();
            if (books.Count() < 1)
            {
                return NotFound(new { message = $"Book with ID not found." });
            }
            return Ok(books);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving books", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<BookDto>> GetBookById(int id)
    {
        try
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound(new { message = $"Book with ID {id} not found." });

            return Ok(book);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving books", error = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BookDto>> CreateBook(BookDto bookDto)
    {
        if (!ModelState.IsValid)
        {
            StatusCode(500, new { message = "Cannot register new user" });
        }

        try
        {
            var newBook = await _bookService.CreateBookAsync(bookDto);
            if (newBook == null)
            {
                return StatusCode(500, new { message = "Cannot Create new book" });
            }

            return Ok(newBook);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating books", error = ex.Message });
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BookDto>> UpdateBook([FromBody] BookDto bookDto)
    {
        if (!ModelState.IsValid)
        {
            StatusCode(500, new { message = "Cannot register new user" });
        }

        try
        {
            var newBook = await _bookService.UpdateBookAsync(bookDto);
            if (newBook == null)
            {
                return StatusCode(500, new { message = "Cannot create new book" });
            }

            return Ok(newBook);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving books", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        if (!ModelState.IsValid)
        {
            StatusCode(500, new { message = "Cannot register new user" });
        }

        try
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return StatusCode(500, new { message = "Book not found" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving books", error = ex.Message });
        }
    }
}
