using SchoolDMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDMS.API.Models.Entities
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Usually roles are pre-seeded
        public RoleEnum RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; }

        // Navigation Property
        public ICollection<User>? Users { get; set; }
    }
}
