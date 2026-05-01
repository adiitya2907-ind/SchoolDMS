using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDMS.API.Models.Entities
{
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UdiseCode { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string SchoolName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string District { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Block { get; set; } = null!;

        [MaxLength(100)]
        public string? State { get; set; }

        [Column(TypeName = "decimal(18, 8)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(18, 8)")]
        public decimal? Longitude { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? ContactPerson { get; set; }

        [MaxLength(20)]
        public string? ContactPhone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Visit>? Visits { get; set; }
    }
}
