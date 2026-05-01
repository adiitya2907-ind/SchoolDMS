using SchoolDMS.API.Models.Enums;

namespace SchoolDMS.API.Models.DTOs.Documents
{
    public class DocumentDTO
    {
        public int DocumentId { get; set; }
        public int VisitId { get; set; }
        public string DocumentType { get; set; } = null!;
        public bool IsMandatory { get; set; }
        public string? FileUrl { get; set; }
        public string? FileName { get; set; }
        public long? FileSize { get; set; }
        public DateTime? UploadedAt { get; set; }
        public string DocumentStatus { get; set; } = null!;
        public string? UploadedBy { get; set; }
    }
}
