using System.ComponentModel.DataAnnotations;

namespace JWTAuthAPI.Dtos
{
    public class AssignRoleDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
