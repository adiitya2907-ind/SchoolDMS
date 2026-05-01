using SchoolDMS.API.Models.Enums;

namespace SchoolDMS.API.Models.DTOs.Visits
{
    public class VisitDTO
    {
        public int VisitId { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; } = null!;
        public string UdiseCode { get; set; } = null!;
        public int EngineerId { get; set; }
        public string EngineerName { get; set; } = null!;
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string VisitType { get; set; } = null!;
        public DateTime VisitDate { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public decimal GpsLatitude { get; set; }
        public decimal? GpsLongitude { get; set; }
        public bool? IsGpsVerified { get; set; }
        public bool? WorkCompleted { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = null!;
        public string? RejectionReason { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
