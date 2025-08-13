using System.ComponentModel.DataAnnotations;

namespace JWTAuthAPI.Dtos
{
    /// <summary>
    /// Data Transfer Object for user registration
    /// This is what we expect when someone wants to create a new account
    /// We separate this from the User model to control what data we accept
    /// </summary>
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string UserName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Data Transfer Object for user login
    /// Simple and focused - we only need email and password for authentication
    /// </summary>
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response object for successful authentication
    /// This is what we send back to the client after successful login
    /// </summary>
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
        public DateTime ExpiresAt { get; set; }
    }
    /// <summary>
    /// User profile information (without sensitive data)
    /// Used when we want to return user info without the password hash
    /// </summary>
    public class UserProfileDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
