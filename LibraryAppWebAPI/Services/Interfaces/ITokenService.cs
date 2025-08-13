using System.Security.Claims;
using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(Member user, List<string> roles);
    ClaimsPrincipal? ValidateToken(string token);
}