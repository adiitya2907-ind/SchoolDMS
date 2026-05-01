namespace SchoolDMS.API.Models.DTOs.Reports
{
    public class ExcelExportDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StatusId { get; set; }
        public string? District { get; set; }
        public int? SchoolId { get; set; }
    }
}
