using AutoMapper;
using JWTAuthAPI.Dtos;
using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryAppWebAPI.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<Member> _userManager;
    private readonly SignInManager<Member> _signInManager;
    private readonly IMapper _mapper;

    public AuthService(UserManager<Member> userManager, SignInManager<Member> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<Member?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user != null)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result.Succeeded)
                return user;
        }
        return null;
    }

    public async Task<Member> SignUpAsync(RegisterDto registerDto)
    {
        if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
            throw new InvalidOperationException("Email is already registered.");

        var member = _mapper.Map<Member>(registerDto);
        member.MembershipId = Guid.NewGuid();
        member.MembershipDate = DateTime.UtcNow;
        member.IsActive = true;

        var result = await _userManager.CreateAsync(member, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to create user: {errors}");
        }

        // Optionally, assign default role
        await _userManager.AddToRoleAsync(member, "Regular");

        return member;
    }

    public async Task<bool> IsEmailRegisteredAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }
}
