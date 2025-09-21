using System.ComponentModel.DataAnnotations;

namespace InsuranceManagement.DTOs
{
    public class CreatePolicyRequest
    {
        [Required]
        [StringLength(50)]
        public string PolicyNumber { get; set; } = string.Empty;

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int AgentId { get; set; }

        [Required]
        [StringLength(50)]
        public string PolicyType { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal PremiumAmount { get; set; }

        [Required]
        public decimal CoverageAmount { get; set; }

        public decimal Deductible { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }

    public class CreatePolicyDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string PolicyType { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Premium amount must be greater than 0")]
        public decimal PremiumAmount { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Coverage amount must be greater than 0")]
        public decimal CoverageAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Deductible cannot be negative")]
        public decimal Deductible { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }

    public class UpdatePolicyDto
    {
        [StringLength(50)]
        public string? Status { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Premium amount must be greater than 0")]
        public decimal? PremiumAmount { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Coverage amount must be greater than 0")]
        public decimal? CoverageAmount { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Deductible cannot be negative")]
        public decimal? Deductible { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
    }

    public class PolicyDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public int AgentId { get; set; }
        public string AgentName { get; set; } = string.Empty;
        public UserDto? Agent { get; set; }
        public string PolicyType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal PremiumAmount { get; set; }
        public decimal CoverageAmount { get; set; }
        public decimal Deductible { get; set; }
        public string? Notes { get; set; }
        public List<CoverageDto> Coverages { get; set; } = new();
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsExpired { get; set; }
        public bool CanBeRenewed { get; set; }
        public DateTime? RenewalDate { get; set; }
        public int? RenewedFromPolicyId { get; set; }
    }



    public class UpdateUserDto
    {
        public int? AgentId { get; set; }
    }

    public class RenewPolicyDto
    {
        [Required]
        public DateTime NewStartDate { get; set; }

        [Required]
        public DateTime NewEndDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Premium amount must be greater than 0")]
        public decimal? NewPremiumAmount { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Coverage amount must be greater than 0")]
        public decimal? NewCoverageAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Deductible cannot be negative")]
        public decimal? NewDeductible { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }

    public class AddressDto
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }

    public class CoverageDto
    {
        public int Id { get; set; }
        public string CoverageType { get; set; } = string.Empty;
        public decimal CoverageAmount { get; set; }
        public decimal Deductible { get; set; }
        public decimal Premium { get; set; }
        public string? Description { get; set; }
    }
}