namespace LibraryAppWebAPI.DTOs;

public class MemberDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public List<string>? Roles { get; set; } = new List<string>();
}