namespace LibraryAppWebAPI.DTOs;

public class SignUpDtos {
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Password { get; set; }
}