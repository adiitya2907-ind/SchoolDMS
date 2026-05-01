using SchoolDMS.API.Models.DTOs.Reports;
using SchoolDMS.API.Models.Responses;

namespace SchoolDMS.API.Services.Interfaces
{
    public interface IReportService
    {
        Task<ApiResponse<DashboardDTO>> GetDashboardMetricsAsync();
        Task<ApiResponse<byte[]>> ExportToExcelAsync(ExcelExportDTO filters);
        Task<ApiResponse<byte[]>> GeneratePdfForVisitAsync(int visitId);
        Task<ApiResponse<byte[]>> GenerateMergedPdfAsync(string visitIds);
    }
}
