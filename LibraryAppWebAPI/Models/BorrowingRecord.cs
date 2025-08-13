namespace LibraryAppWebAPI.Models;

using LibraryAppWebAPI.Enums;

public class BorrowingRecord
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal? FineAmount { get; set; }
    public Status Status { get; set; } = Status.AVAILABLE;
    public Book? Book { get; set; }
    public Member? Member { get; set; }
}
