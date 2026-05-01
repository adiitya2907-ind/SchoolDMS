using SchoolDMS.API.Models.Enums;

namespace SchoolDMS.API.Models.DTOs.Visits
{
    public class CreateVisitDTO
    {
        public int SchoolId { get; set; }
        public int ProjectId { get; set; }
        public VisitTypeEnum VisitType { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
