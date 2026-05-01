using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDMS.API.Models.Entities
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditId { get; set; }

        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [Required]
        [MaxLength(50)]
        public string Action { get; set; } = null!; // Created, Updated, Deleted, Approved, Rejected

        [Required]
        [MaxLength(100)]
        public string TableName { get; set; } = null!;

        public int? RecordId { get; set; }

        public string? OldValues { get; set; } // Stored as JSON string

        public string? NewValues { get; set; } // Stored as JSON string

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
