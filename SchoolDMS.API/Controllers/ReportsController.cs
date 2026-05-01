using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDMS.API.Models.DTOs.Reports;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "OpsVerifier,Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardMetrics()
        {
            var result = await _reportService.GetDashboardMetricsAsync();
            return Ok(result);
        }

        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportToExcel([FromQuery] ExcelExportDTO filters)
        {
            var result = await _reportService.ExportToExcelAsync(filters);
            if (!result.Success || result.Data == null) return BadRequest(result);

            return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VisitsExport.xlsx");
        }

        [HttpGet("generate-pdf/{visitId}")]
        public async Task<IActionResult> GeneratePdf(int visitId)
        {
            var result = await _reportService.GeneratePdfForVisitAsync(visitId);
            if (!result.Success || result.Data == null) return BadRequest(result);

            return File(result.Data, "application/pdf", $"Visit_{visitId}.pdf");
        }

        [HttpGet("generate-merged-pdf")]
        public async Task<IActionResult> GenerateMergedPdf([FromQuery] string visitIds)
        {
            var result = await _reportService.GenerateMergedPdfAsync(visitIds);
            if (!result.Success || result.Data == null) return BadRequest(result);

            return File(result.Data, "application/pdf", "MergedVisits.pdf");
        }
    }
}
