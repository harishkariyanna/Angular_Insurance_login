using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceManagement.Models
{
    public class Policy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PolicyNumber { get; set; } = string.Empty;

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;

        [Required]
        public int AgentId { get; set; }

        [ForeignKey("AgentId")]
        public virtual User Agent { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string PolicyType { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PremiumAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CoverageAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductible { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        // Navigation properties
        public virtual ICollection<Coverage> Coverages { get; set; } = new List<Coverage>();
        public virtual ICollection<Premium> Premiums { get; set; } = new List<Premium>();
        public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    }

    public class Coverage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PolicyId { get; set; }

        [ForeignKey("PolicyId")]
        public virtual Policy Policy { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string CoverageType { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CoverageAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductible { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Premium { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }

    public class Premium
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PolicyId { get; set; }

        [ForeignKey("PolicyId")]
        public virtual Policy Policy { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? PaidDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [StringLength(100)]
        public string? TransactionId { get; set; }
    }
}