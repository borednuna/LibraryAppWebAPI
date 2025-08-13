using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryAppWebAPI.Enums;

namespace LibraryAppWebAPI.Models;

public class BorrowingRecord
{
    public int Id { get; set; }

    [Required(ErrorMessage = "BookId is required")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "MemberId is required")]
    public Guid MemberId { get; set; }

    [Required(ErrorMessage = "BorrowDate is required")]
    public DateTime BorrowDate { get; set; }

    [Required(ErrorMessage = "DueDate is required")]
    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public DateTime? DeletedAt { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "FineAmount cannot be negative")]
    public decimal? FineAmount { get; set; }

    [Required]
    public Status Status { get; set; } = Status.AVAILABLE;

    [ForeignKey("BookId")]
    public virtual Book? Book { get; set; }

    [ForeignKey("MemberId")]
    public virtual Member? Member { get; set; }
}
