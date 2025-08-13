namespace LibraryAppWebAPI.Services.Interfaces;

using LibraryAppWebAPI.Models;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetAllMembersAsync();
    Task<Member?> GetMemberByIdAsync(int id);
    Task<Member?> GetMemberByMembershipIdAsync(Guid membershipId);
    Task<Member> CreateMemberAsync(Member member);
    Task<Member?> UpdateMemberAsync(Member member);
    Task<bool> DeleteMemberAsync(int id);
    Task<IEnumerable<Member>> GetActiveMembersAsync();
    Task<IEnumerable<Member>> GetInactiveMembersAsync();
    Task<bool> SetMemberStatusAsync(int id, bool isActive);
}
