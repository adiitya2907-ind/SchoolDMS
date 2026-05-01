namespace SchoolDMS.API.Models.DTOs.Visits
{
    public class VisitApprovalDTO
    {
        public bool IsApproved { get; set; }
        public string? RejectionReasons { get; set; } // Comma separated if rejected
        public string? Comments { get; set; }
    }
}
