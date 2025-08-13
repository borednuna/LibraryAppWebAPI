using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryAppWebAPI.Enums;
using Microsoft.AspNetCore.Identity;

namespace LibraryAppWebAPI.Models;

public class Member : IdentityUser
{
    [Required]
    public Guid MembershipId { get; set; }

    [Required]
    public DateTime MembershipDate { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public virtual ICollection<BorrowingRecord> BorrowingHistory { get; set; } = new List<BorrowingRecord>();
}
