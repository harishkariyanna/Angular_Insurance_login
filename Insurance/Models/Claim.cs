using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagement.Models
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ClaimNumber { get; set; } = string.Empty;

        [Required]
        public int PolicyId { get; set; }

        [ForeignKey("PolicyId")]
        public virtual Policy Policy { get; set; } = null!;

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;

        public int? AssignedAgentId { get; set; }

        [ForeignKey("AssignedAgentId")]
        public virtual User? AssignedAgent { get; set; }

        [Required]
        [StringLength(50)]
        public string ClaimType { get; set; } = string.Empty;

        [Required]
        public DateTime IncidentDate { get; set; }

        [Required]
        public DateTime ReportedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ClaimAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ApprovedAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Submitted";

        [Required]
        [StringLength(50)]
        public string Priority { get; set; } = "Medium";

        [StringLength(200)]
        public string? IncidentLocation { get; set; }

        [StringLength(500)]
        public string? PoliceReportNumber { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<ClaimNote> Notes { get; set; } = new List<ClaimNote>();
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    }

    public class ClaimNote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClaimId { get; set; }

        [ForeignKey("ClaimId")]
        public virtual Claim Claim { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [Required]
        [StringLength(2000)]
        public string Note { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public long FileSize { get; set; }

        [Required]
        [StringLength(100)]
        public string ContentType { get; set; } = string.Empty;

        [Required]
        public byte[] FileData { get; set; } = Array.Empty<byte>();

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int UploadedBy { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        // Foreign keys for polymorphic relationship
        public int? PolicyId { get; set; }
        public int? ClaimId { get; set; }

        [ForeignKey("PolicyId")]
        public virtual Policy? Policy { get; set; }

        [ForeignKey("ClaimId")]
        public virtual Claim? Claim { get; set; }
    }
}