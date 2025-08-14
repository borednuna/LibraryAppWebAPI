using System.ComponentModel.DataAnnotations;
using LibraryAppWebAPI.Enums;

namespace LibraryAppWebAPI.DTOs;

public class BookDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Author must be between 1 and 100 characters")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN is required")]
    [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 13 characters")]
    [RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "ISBN must be numeric, 10 or 13 digits")]
    public string ISBN { get; set; } = string.Empty;


    [Required(ErrorMessage = "Genre is required")]
    public Genre Genre { get; set; }
}
