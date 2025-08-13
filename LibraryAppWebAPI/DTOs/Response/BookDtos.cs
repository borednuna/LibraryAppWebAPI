using LibraryAppWebAPI.Enums;

namespace LibraryAppWebAPI.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public DateTime? DeletedAt { get; set; }
    public int? RackId { get; set; }
    public Genre Genre { get; set; }

}