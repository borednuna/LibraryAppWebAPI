using FluentValidation;
using JWTAuthAPI.Dtos;
using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Services.Interfaces;
using LibraryAppWebAPI.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<Member> _userManager;
    private readonly SignInManager<Member> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private IValidator<RegisterDto> _registerValidator;
    private IValidator<LoginDto> _loginValidator;

    public AuthController(
        UserManager<Member> userManager,
        SignInManager<Member> signInManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService,
        IValidator<RegisterDto> registerValidator,
        IValidator<LoginDto> loginValidator
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            StatusCode(500, new { message = "Cannot register new user" });
        }

        try
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "Email is already registered." });
            }

            var newUser = new Member
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                MembershipId = Guid.NewGuid(),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { message = "Registration failed", errors });
            }

            await _userManager.AddToRoleAsync(newUser, "User");

            return CreatedAtAction(nameof(GetProfile), new { }, new
            {
                message = "User registered successfully",
                userId = newUser.Id,
                email = newUser.Email
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Registration failed due to server error" });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            StatusCode(500, new { message = "Cannot login" });
        }

        try
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    return Unauthorized(new { message = "Account is locked out." });

                return Unauthorized(new { message = "Invalid credentials." });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles.ToList());

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email ?? "",
                UserName = user.UserName ?? "Default User",
                Roles = roles.ToList(),
                ExpiresAt = DateTime.UtcNow.AddDays(1)
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Login failed due to server error" });
        }
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid token." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new MemberDto
            {
                Email = user.Email ?? "",
                UserName = user.UserName ?? "Default User",
                Roles = roles.ToList()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to retrieve profile" });
        }
    }

    [HttpPost("assign-role")]
    [Authorize]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleDTO model)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            if (!await _roleManager.RoleExistsAsync(model.Role))
                return BadRequest(new { message = "Role does not exist" });

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok(new { message = $"Role {model.Role} assigned to user successfully" });
            }

            return BadRequest(new { message = "Failed to assign role", errors = result.Errors });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to assign role" });
        }
    }
}
