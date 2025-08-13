using JWTAuthAPI.Dtos;
using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Models;
using System.Threading.Tasks;

namespace LibraryAppWebAPI.Services.Interfaces;

public interface IAuthService
{
    // Login menggunakan Email dan Password dari LoginDto
    Task<Member?> LoginAsync(LoginDto loginDto);

    // Registrasi user baru dari RegisterDto
    Task<Member> SignUpAsync(RegisterDto registerDto);

    // Cek apakah email sudah terdaftar
    Task<bool> IsEmailRegisteredAsync(string email);
}
