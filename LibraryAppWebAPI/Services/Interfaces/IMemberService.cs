using JWTAuthAPI.Dtos;
using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.Services.Interfaces;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetAllMembersAsync();
    Task<Member?> GetMemberByIdAsync(string id); // Id jadi string
    Task<Member?> GetMemberByMembershipIdAsync(Guid membershipId);
    Task<Member> CreateMemberAsync(RegisterDto registerDto); // menerima DTO
    // Task<Member?> UpdateMemberAsync(Member member);
    Task<bool> DeleteMemberAsync(string id); // Id jadi string
    Task<IEnumerable<Member>> GetActiveMembersAsync();
    Task<IEnumerable<Member>> GetInactiveMembersAsync();
    Task<bool> SetMemberStatusAsync(string id, bool isActive); // Id jadi string
}
