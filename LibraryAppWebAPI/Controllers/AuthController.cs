using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpDtos signUpDto)
    {
        try
        {
            var memberDto = await _authService.SignUpAsync(signUpDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving books", error = ex.Message });
        }
    }
}
