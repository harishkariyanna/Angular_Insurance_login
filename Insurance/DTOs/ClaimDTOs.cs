using System.ComponentModel.DataAnnotations;

namespace InsuranceManagement.DTOs
{
    public class ClaimDto
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public PolicyDto Policy { get; set; } = null!;
        public UserDto Customer { get; set; } = null!;
        public UserDto? AssignedAgent { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public DateTime IncidentDate { get; set; }
        public DateTime ReportedDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string? IncidentLocation { get; set; }
        public string? PoliceReportNumber { get; set; }
        public List<ClaimNoteDto> Notes { get; set; } = new();
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class CreateClaimDto
    {
        [Required]
        public int PolicyId { get; set; }

        [Required]
        [StringLength(50)]
        public string ClaimType { get; set; } = string.Empty;

        [Required]
        public DateTime IncidentDate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 1000000)]
        public decimal ClaimAmount { get; set; }

        [StringLength(200)]
        public string? IncidentLocation { get; set; }

        [StringLength(500)]
        public string? PoliceReportNumber { get; set; }
    }

    public class UpdateClaimDto
    {
        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? Priority { get; set; }

        public decimal? ClaimAmount { get; set; }
        public string? Description { get; set; }
    }

    public class ApproveClaimDto
    {
        [Required]
        [Range(0.01, 1000000)]
        public decimal ApprovedAmount { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }

    public class DenyClaimDto
    {
        [Required]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;
    }

    public class ClaimNoteDto
    {
        public int Id { get; set; }
        public string Note { get; set; } = string.Empty;
        public UserDto User { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }

    public class CreateClaimNoteDto
    {
        [Required]
        [StringLength(2000)]
        public string Note { get; set; } = string.Empty;
    }
}