using SchoolDMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDMS.API.Models.Entities
{
    public class Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitId { get; set; }

        [Required]
        public int SchoolId { get; set; }
        
        [ForeignKey(nameof(SchoolId))]
        public School? School { get; set; }

        [Required]
        public int EngineerId { get; set; }

        [ForeignKey(nameof(EngineerId))]
        public User? Engineer { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }

        [Required]
        public VisitTypeEnum VisitType { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 8)")]
        public decimal GpsLatitude { get; set; }

        [Column(TypeName = "decimal(18, 8)")]
        public decimal? GpsLongitude { get; set; }

        public bool? IsGpsVerified { get; set; }
        public bool? WorkCompleted { get; set; }

        [MaxLength(2000)]
        public string? Notes { get; set; }

        [Required]
        public VisitStatusEnum Status { get; set; } = VisitStatusEnum.Draft;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [MaxLength(1000)]
        public string? RejectionReason { get; set; }

        // Navigation Properties
        public ICollection<Document>? Documents { get; set; }
        public ICollection<ApprovalWorkflow>? ApprovalWorkflows { get; set; }
    }
}
