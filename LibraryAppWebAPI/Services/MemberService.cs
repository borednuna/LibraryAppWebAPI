namespace LibraryAppWebAPI.Services;

using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Repositories.Interfaces;
using LibraryAppWebAPI.Services.Interfaces;
using BCrypt.Net;

public class MemberService(IMemberRepository memberRepository) : IMemberService
{
    private readonly IMemberRepository _memberRepository = memberRepository;

    public async Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        return await _memberRepository.GetAllAsync();
    }

    public async Task<Member?> GetMemberByIdAsync(int id)
    {
        return await _memberRepository.GetByIdAsync(id);
    }

    public async Task<Member?> GetMemberByMembershipIdAsync(Guid membershipId)
    {
        return await _memberRepository.GetByMembershipIdAsync(membershipId);
    }

    public async Task<Member> CreateMemberAsync(Member member)
    {
        member.MembershipId = Guid.NewGuid();
        member.Password = BCrypt.HashPassword(member.Password);
        return await _memberRepository.AddAsync(member);
    }

    public async Task<Member?> UpdateMemberAsync(Member member)
    {
        if (!string.IsNullOrWhiteSpace(member.Password))
        {
            member.Password = BCrypt.HashPassword(member.Password);
        }

        return await _memberRepository.UpdateAsync(member);
    }

    public async Task<bool> DeleteMemberAsync(int id)
    {
        return await _memberRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Member>> GetActiveMembersAsync()
    {
        var members = await _memberRepository.GetAllAsync();
        return members.Where(m => m.IsActive);
    }

    public async Task<IEnumerable<Member>> GetInactiveMembersAsync()
    {
        var members = await _memberRepository.GetAllAsync();
        return members.Where(m => !m.IsActive);
    }

    public async Task<bool> SetMemberStatusAsync(int id, bool isActive)
    {
        var member = await _memberRepository.GetByIdAsync(id);
        if (member == null)
            return false;

        member.IsActive = isActive;
        await _memberRepository.UpdateAsync(member);
        return true;
    }
}
