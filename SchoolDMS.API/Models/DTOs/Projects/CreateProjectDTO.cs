namespace SchoolDMS.API.Models.DTOs.Projects
{
    public class CreateProjectDTO
    {
        public string ProjectName { get; set; } = null!;
        public string? Description { get; set; }
        public string Status { get; set; } = "Active";
    }
}
