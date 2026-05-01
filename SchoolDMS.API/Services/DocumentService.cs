using AutoMapper;
using Microsoft.Extensions.Configuration;
using SchoolDMS.API.Models.DTOs.Documents;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Enums;
using SchoolDMS.API.Models.Responses;
using SchoolDMS.API.Repositories.Interfaces;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DocumentService(IDocumentRepository documentRepository, IVisitRepository visitRepository, IConfiguration configuration, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _visitRepository = visitRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ApiResponse<DocumentDTO>> UploadDocumentAsync(DocumentUploadDTO request, int userId)
        {
            var visit = await _visitRepository.GetByIdAsync(request.VisitId);
            if (visit == null || visit.EngineerId != userId)
                return ApiResponse<DocumentDTO>.FailureResponse("Visit not found or unauthorized", 404);

            if (visit.Status != VisitStatusEnum.Draft)
                return ApiResponse<DocumentDTO>.FailureResponse("Can only upload documents for draft visits", 400);

            if (request.File == null || request.File.Length == 0)
                return ApiResponse<DocumentDTO>.FailureResponse("Invalid file", 400);

            // File validation
            var maxFileSize = _configuration.GetValue<long>("FileUpload:MaxFileSize");
            var allowedExtensions = _configuration.GetSection("FileUpload:AllowedExtensions").Get<string[]>();
            var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();

            if (request.File.Length > maxFileSize)
                return ApiResponse<DocumentDTO>.FailureResponse($"File size exceeds limit of {maxFileSize / 1024 / 1024}MB", 400);

            if (allowedExtensions != null && !allowedExtensions.Contains(extension))
                return ApiResponse<DocumentDTO>.FailureResponse("Invalid file extension", 400);

            // Save file
            var storagePath = _configuration.GetValue<string>("FileUpload:StoragePath") ?? "uploads/";
            if (!Directory.Exists(storagePath))
                Directory.CreateDirectory(storagePath);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(storagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var document = new Document
            {
                VisitId = request.VisitId,
                DocumentType = request.DocumentType,
                FileName = request.File.FileName,
                FileUrl = filePath,
                FileSize = request.File.Length,
                UploadedAt = DateTime.UtcNow,
                UploadedById = userId,
                DocumentStatus = DocumentStatusEnum.Uploaded,
                IsMandatory = true // Simplification: we handle logic in ValidationHelper
            };

            await _documentRepository.AddAsync(document);
            await _documentRepository.SaveChangesAsync();

            var result = await _documentRepository.GetByIdAsync(document.DocumentId);
            return ApiResponse<DocumentDTO>.SuccessResponse(_mapper.Map<DocumentDTO>(result), "Document uploaded", 201);
        }

        public async Task<ApiResponse<IEnumerable<DocumentDTO>>> GetDocumentsByVisitIdAsync(int visitId)
        {
            var documents = await _documentRepository.GetDocumentsByVisitIdAsync(visitId);
            return ApiResponse<IEnumerable<DocumentDTO>>.SuccessResponse(_mapper.Map<IEnumerable<DocumentDTO>>(documents));
        }

        public async Task<ApiResponse<DocumentDTO>> GetDocumentByIdAsync(int documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);
            if (document == null) return ApiResponse<DocumentDTO>.FailureResponse("Document not found", 404);

            return ApiResponse<DocumentDTO>.SuccessResponse(_mapper.Map<DocumentDTO>(document));
        }

        public async Task<ApiResponse<bool>> DeleteDocumentAsync(int documentId, int userId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);
            if (document == null) return ApiResponse<bool>.FailureResponse("Document not found", 404);

            var visit = await _visitRepository.GetByIdAsync(document.VisitId);
            if (visit == null || visit.EngineerId != userId || visit.Status != VisitStatusEnum.Draft)
                return ApiResponse<bool>.FailureResponse("Unauthorized or visit not in draft", 403);

            // Delete file from disk
            if (File.Exists(document.FileUrl))
            {
                File.Delete(document.FileUrl);
            }

            _documentRepository.Remove(document);
            await _documentRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Document deleted");
        }

        public async Task<ApiResponse<bool>> VerifyDocumentAsync(int documentId, bool isVerified, int verifierId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);
            if (document == null) return ApiResponse<bool>.FailureResponse("Document not found", 404);

            document.DocumentStatus = isVerified ? DocumentStatusEnum.Verified : DocumentStatusEnum.Rejected;
            _documentRepository.Update(document);
            await _documentRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Document verification status updated");
        }
    }
}
