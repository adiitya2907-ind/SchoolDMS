using SchoolDMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDMS.API.Models.Entities
{
    public class ApprovalWorkflow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApprovalId { get; set; }

        [Required]
        public int VisitId { get; set; }

        [ForeignKey(nameof(VisitId))]
        public Visit? Visit { get; set; }

        [Required]
        public int VerifierId { get; set; }

        [ForeignKey(nameof(VerifierId))]
        public User? Verifier { get; set; }

        [Required]
        public ApprovalStatusEnum ApprovalStatus { get; set; } = ApprovalStatusEnum.Pending;

        [MaxLength(500)]
        public string? RejectionReasons { get; set; } // Comma separated values

        [MaxLength(1000)]
        public string? Comments { get; set; }

        public DateTime? ApprovedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
