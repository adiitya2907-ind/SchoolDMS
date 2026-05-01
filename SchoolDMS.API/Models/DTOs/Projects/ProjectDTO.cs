namespace SchoolDMS.API.Models.DTOs.Projects
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string? Description { get; set; }
        public string Status { get; set; } = null!;
    }
}
