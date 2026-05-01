using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDMS.API.Models.Entities
{
    public class DocumentSearch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SearchId { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [ForeignKey(nameof(DocumentId))]
        public Document? Document { get; set; }

        public string? ExtractedText { get; set; }
        
        [MaxLength(1000)]
        public string? SearchIndex { get; set; }

        public DateTime? LastIndexedAt { get; set; }
    }
}
