namespace SchoolDMS.API.Models.DTOs.Reports
{
    public class DashboardDTO
    {
        public int TotalVisits { get; set; }
        public int PendingVerificationCount { get; set; }
        public int CompletedVisitsCount { get; set; }
        public int RepeatVisitsCount { get; set; }
    }
}
