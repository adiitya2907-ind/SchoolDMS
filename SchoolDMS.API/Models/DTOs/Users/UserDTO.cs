namespace SchoolDMS.API.Models.DTOs.Users
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; } = null!;
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
