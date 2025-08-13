using JWTAuthAPI.Dtos;
using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryAppWebAPI.Services;

public class MemberService : IMemberService
{
    private readonly UserManager<Member> _userManager;

    public MemberService(UserManager<Member> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        return _userManager.Users.ToList();
    }

    public async Task<Member?> GetMemberByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<Member?> GetMemberByMembershipIdAsync(Guid membershipId)
    {
        return _userManager.Users.FirstOrDefault(m => m.MembershipId == membershipId);
    }

    public async Task<Member> CreateMemberAsync(RegisterDto registerDto)
    {
        var member = new Member
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            MembershipId = Guid.NewGuid(),
            MembershipDate = DateTime.UtcNow,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(member, registerDto.Password);
        if (!result.Succeeded)
            throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));

        return member;
    }

    // public async Task<Member?> UpdateMemberAsync(Member member)
    // {
    //     var existingMember = await _userManager.FindByIdAsync(member.Id);
    //     if (existingMember == null)
    //         return null;

    //     existingMember.Email = member.Email;
    //     existingMember.UserName = member.Email;

    //     if (!string.IsNullOrWhiteSpace(member.Password))
    //     {
    //         var token = await _userManager.GeneratePasswordResetTokenAsync(existingMember);
    //         var resetResult = await _userManager.ResetPasswordAsync(existingMember, token, member.Password);
    //         if (!resetResult.Succeeded)
    //             throw new InvalidOperationException(string.Join("; ", resetResult.Errors.Select(e => e.Description)));
    //     }

    //     var updateResult = await _userManager.UpdateAsync(existingMember);
    //     if (!updateResult.Succeeded)
    //         throw new InvalidOperationException(string.Join("; ", updateResult.Errors.Select(e => e.Description)));

    //     return existingMember;
    // }

    public async Task<bool> DeleteMemberAsync(string id)
    {
        var member = await _userManager.FindByIdAsync(id);
        if (member == null) return false;

        var result = await _userManager.DeleteAsync(member);
        return result.Succeeded;
    }

    public async Task<bool> SetMemberStatusAsync(string id, bool isActive)
    {
        var member = await _userManager.FindByIdAsync(id);
        if (member == null) return false;

        member.IsActive = isActive;
        var result = await _userManager.UpdateAsync(member);
        return result.Succeeded;
    }

    public async Task<IEnumerable<Member>> GetActiveMembersAsync()
    {
        return _userManager.Users.Where(m => m.IsActive).ToList();
    }

    public async Task<IEnumerable<Member>> GetInactiveMembersAsync()
    {
        return _userManager.Users.Where(m => !m.IsActive).ToList();
    }
}
