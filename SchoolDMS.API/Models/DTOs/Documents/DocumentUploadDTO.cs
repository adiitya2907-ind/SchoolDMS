using Microsoft.AspNetCore.Http;
using SchoolDMS.API.Models.Enums;

namespace SchoolDMS.API.Models.DTOs.Documents
{
    public class DocumentUploadDTO
    {
        public int VisitId { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}
