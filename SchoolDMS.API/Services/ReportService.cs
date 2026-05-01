using OfficeOpenXml;
using SchoolDMS.API.Models.DTOs.Reports;
using SchoolDMS.API.Models.Enums;
using SchoolDMS.API.Models.Responses;
using SchoolDMS.API.Repositories.Interfaces;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Services
{
    public class ReportService : IReportService
    {
        private readonly IVisitRepository _visitRepository;

        public ReportService(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<ApiResponse<DashboardDTO>> GetDashboardMetricsAsync()
        {
            var visits = await _visitRepository.GetAllAsync();

            var metrics = new DashboardDTO
            {
                TotalVisits = visits.Count(),
                PendingVerificationCount = visits.Count(v => v.Status == VisitStatusEnum.PendingVerification),
                CompletedVisitsCount = visits.Count(v => v.Status == VisitStatusEnum.Approved),
                RepeatVisitsCount = visits.GroupBy(v => v.SchoolId).Count(g => g.Count() > 1)
            };

            return ApiResponse<DashboardDTO>.SuccessResponse(metrics);
        }

        public async Task<ApiResponse<byte[]>> ExportToExcelAsync(ExcelExportDTO filters)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var visits = await _visitRepository.GetVisitsWithDetailsAsync(null, filters.SchoolId, filters.StatusId);

            if (filters.StartDate.HasValue)
                visits = visits.Where(v => v.VisitDate >= filters.StartDate.Value);

            if (filters.EndDate.HasValue)
                visits = visits.Where(v => v.VisitDate <= filters.EndDate.Value);

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Visits");

            // Add Headers
            worksheet.Cells[1, 1].Value = "Visit ID";
            worksheet.Cells[1, 2].Value = "School Name";
            worksheet.Cells[1, 3].Value = "UDISE Code";
            worksheet.Cells[1, 4].Value = "Engineer";
            worksheet.Cells[1, 5].Value = "Visit Date";
            worksheet.Cells[1, 6].Value = "Status";

            int row = 2;
            foreach (var visit in visits)
            {
                worksheet.Cells[row, 1].Value = visit.VisitId;
                worksheet.Cells[row, 2].Value = visit.School?.SchoolName;
                worksheet.Cells[row, 3].Value = visit.School?.UdiseCode;
                worksheet.Cells[row, 4].Value = visit.Engineer != null ? $"{visit.Engineer.FirstName} {visit.Engineer.LastName}" : "";
                worksheet.Cells[row, 5].Value = visit.VisitDate.ToString("yyyy-MM-dd");
                worksheet.Cells[row, 6].Value = visit.Status.ToString();
                row++;
            }

            worksheet.Cells.AutoFitColumns();
            return ApiResponse<byte[]>.SuccessResponse(package.GetAsByteArray());
        }

        public async Task<ApiResponse<byte[]>> GeneratePdfForVisitAsync(int visitId)
        {
            // Placeholder for PDF generation logic (e.g., using iTextSharp)
            byte[] pdfBytes = System.Text.Encoding.UTF8.GetBytes($"PDF Content for Visit {visitId}");
            return await Task.FromResult(ApiResponse<byte[]>.SuccessResponse(pdfBytes));
        }

        public async Task<ApiResponse<byte[]>> GenerateMergedPdfAsync(string visitIds)
        {
            // Placeholder for Merged PDF logic
            byte[] pdfBytes = System.Text.Encoding.UTF8.GetBytes($"Merged PDF for Visits: {visitIds}");
            return await Task.FromResult(ApiResponse<byte[]>.SuccessResponse(pdfBytes));
        }
    }
}
