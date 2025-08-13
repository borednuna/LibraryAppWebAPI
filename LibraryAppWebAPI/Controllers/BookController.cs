using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService bookService) : ControllerBase
{
    private readonly IBookService _bookService = bookService;

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
}