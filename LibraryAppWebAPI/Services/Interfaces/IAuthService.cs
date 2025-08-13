namespace LibraryAppWebAPI.Services.Interfaces;

using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Models;

public interface IAuthService
{
    Task<Member?> LoginAsync(string email, string password);
    Task<Member> SignUpAsync(SignUpDtos signUpDto);
    Task<bool> IsEmailRegisteredAsync(string email);
}
