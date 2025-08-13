namespace LibraryAppWebAPI.Models;

public class Member
{
    public int Id { get; set; }
    public Guid MembershipId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Password { get; set; }
    public DateTime MembershipDate { get; set; }
    public bool IsActive { get; set; } = true;
    
    public virtual ICollection<BorrowingRecord> BorrowingHistory { get; set; } = new List<BorrowingRecord>();
}
