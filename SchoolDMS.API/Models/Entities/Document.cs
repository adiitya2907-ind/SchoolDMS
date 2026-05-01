using SchoolDMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDMS.API.Models.Entities
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentId { get; set; }

        [Required]
        public int VisitId { get; set; }

        [ForeignKey(nameof(VisitId))]
        public Visit? Visit { get; set; }

        [Required]
        public DocumentTypeEnum DocumentType { get; set; }

        public bool IsMandatory { get; set; }

        [MaxLength(500)]
        public string? FileUrl { get; set; }

        [MaxLength(255)]
        public string? FileName { get; set; }

        public long? FileSize { get; set; }

        public DateTime? UploadedAt { get; set; }

        public int? UploadedById { get; set; }

        [ForeignKey(nameof(UploadedById))]
        public User? UploadedBy { get; set; }

        [Required]
        public DocumentStatusEnum DocumentStatus { get; set; } = DocumentStatusEnum.Pending;

        // Navigation Properties
        public ICollection<DocumentSearch>? DocumentSearches { get; set; }
    }
}
