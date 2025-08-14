using System.ComponentModel.DataAnnotations;
using LibraryAppWebAPI.Enums;

namespace LibraryAppWebAPI.DTOs;

public class BookDtoResponse
{
    public int? Id { get; set; }
    public string Image { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;
    public Genre Genre { get; set; }
}
