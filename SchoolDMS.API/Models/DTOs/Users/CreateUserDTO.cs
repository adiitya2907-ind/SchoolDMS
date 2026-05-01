using SchoolDMS.API.Models.Enums;

namespace SchoolDMS.API.Models.DTOs.Users
{
    public class CreateUserDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
        public RoleEnum RoleId { get; set; }
    }
}
