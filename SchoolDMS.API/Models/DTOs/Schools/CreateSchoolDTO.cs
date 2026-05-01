namespace SchoolDMS.API.Models.DTOs.Schools
{
    public class CreateSchoolDTO
    {
        public string UdiseCode { get; set; } = null!;
        public string SchoolName { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Block { get; set; } = null!;
        public string? State { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
    }
}
