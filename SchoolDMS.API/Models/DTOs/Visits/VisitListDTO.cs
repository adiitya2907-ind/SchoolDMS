using SchoolDMS.API.Models.Enums;

namespace SchoolDMS.API.Models.DTOs.Visits
{
    public class VisitListDTO
    {
        public int VisitId { get; set; }
        public string SchoolName { get; set; } = null!;
        public string UdiseCode { get; set; } = null!;
        public string EngineerName { get; set; } = null!;
        public string VisitType { get; set; } = null!;
        public DateTime VisitDate { get; set; }
        public string Status { get; set; } = null!;
        public bool? IsGpsVerified { get; set; }
    }
}
