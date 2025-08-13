namespace LibraryAppWebAPI.Models;

using System.ComponentModel.DataAnnotations;
using LibraryAppWebAPI.Enums;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    [MinLength(10)]
    [MaxLength(13)]
    public string ISBN { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public DateTime? DeletedAt { get; set; }
    public int? RackId { get; set; }
    public Genre Genre { get; set; }
    public Rack? Rack { get; set; }
    public virtual ICollection<BorrowingRecord> BorrowingHistory { get; set; } = new List<BorrowingRecord>();
}
