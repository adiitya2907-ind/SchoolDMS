using SchoolDMS.API.Models.Enums;

namespace SchoolDMS.API.Models.DTOs.Users
{
    public class UpdateUserDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public RoleEnum RoleId { get; set; }
    }
}
