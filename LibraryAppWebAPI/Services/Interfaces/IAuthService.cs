namespace LibraryAppWebAPI.Services.Interfaces;

using LibraryAppWebAPI.Models;

public interface IAuthService
{
    Task<Member?> LoginAsync(string email, string password);
    Task<Member> SignUpAsync(string name, string email, string password, DateTime membershipDate);
    Task<bool> IsEmailRegisteredAsync(string email);
}
