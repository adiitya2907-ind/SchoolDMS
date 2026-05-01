using SchoolDMS.API.Models.DTOs.Documents;
using SchoolDMS.API.Models.Responses;

namespace SchoolDMS.API.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<ApiResponse<DocumentDTO>> UploadDocumentAsync(DocumentUploadDTO request, int userId);
        Task<ApiResponse<IEnumerable<DocumentDTO>>> GetDocumentsByVisitIdAsync(int visitId);
        Task<ApiResponse<DocumentDTO>> GetDocumentByIdAsync(int documentId);
        Task<ApiResponse<bool>> DeleteDocumentAsync(int documentId, int userId);
        Task<ApiResponse<bool>> VerifyDocumentAsync(int documentId, bool isVerified, int verifierId);
    }
}
