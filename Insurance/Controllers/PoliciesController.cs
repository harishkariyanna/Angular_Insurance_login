using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsuranceManagement.Data;
using InsuranceManagement.Models;
using InsuranceManagement.DTOs;

namespace InsuranceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PoliciesController : ControllerBase
    {
        private readonly InsuranceDbContext _context;

        public PoliciesController(InsuranceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PolicyDto>>> GetPolicies()
        {
            var policies = await _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.Agent)
                .Select(p => new PolicyDto
                {
                    Id = p.Id,
                    PolicyNumber = p.PolicyNumber,
                    CustomerId = p.CustomerId,
                    CustomerName = $"{p.Customer.FirstName} {p.Customer.LastName}",
                    AgentId = p.AgentId,
                    AgentName = $"{p.Agent.FirstName} {p.Agent.LastName}",
                    PolicyType = p.PolicyType,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status,
                    PremiumAmount = p.PremiumAmount,
                    CoverageAmount = p.CoverageAmount,
                    Deductible = p.Deductible,
                    Notes = p.Notes,
                    CreatedDate = p.CreatedDate
                })
                .ToListAsync();
            
            return Ok(policies);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PolicyDto>> CreatePolicy(CreatePolicyRequest request)
        {
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            if (userRole != "Admin" && userRole != "Agent")
                return Forbid("Only Admin and Agent users can create policies");

            var policy = new Policy
            {
                PolicyNumber = request.PolicyNumber,
                CustomerId = request.CustomerId,
                AgentId = request.AgentId,
                PolicyType = request.PolicyType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PremiumAmount = request.PremiumAmount,
                CoverageAmount = request.CoverageAmount,
                Deductible = request.Deductible,
                Notes = request.Notes,
                Status = "Active",
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow
            };

            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();

            var createdPolicy = await _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.Agent)
                .Where(p => p.Id == policy.Id)
                .Select(p => new PolicyDto
                {
                    Id = p.Id,
                    PolicyNumber = p.PolicyNumber,
                    CustomerId = p.CustomerId,
                    CustomerName = $"{p.Customer.FirstName} {p.Customer.LastName}",
                    AgentId = p.AgentId,
                    AgentName = $"{p.Agent.FirstName} {p.Agent.LastName}",
                    PolicyType = p.PolicyType,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status,
                    PremiumAmount = p.PremiumAmount,
                    CoverageAmount = p.CoverageAmount,
                    Deductible = p.Deductible,
                    Notes = p.Notes,
                    CreatedDate = p.CreatedDate
                })
                .FirstAsync();

            return CreatedAtAction(nameof(GetPolicies), new { id = policy.Id }, createdPolicy);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePolicy(int id, UpdatePolicyDto request)
        {
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            if (userRole != "Admin" && userRole != "Agent")
                return Forbid("Only Admin and Agent users can update policies");

            try
            {
                var policy = await _context.Policies.FindAsync(id);
                if (policy == null)
                    return NotFound($"Policy with ID {id} not found");

                // Update only provided fields
                if (!string.IsNullOrEmpty(request.Status))
                    policy.Status = request.Status;
                if (request.PremiumAmount.HasValue)
                    policy.PremiumAmount = request.PremiumAmount.Value;
                if (request.CoverageAmount.HasValue)
                    policy.CoverageAmount = request.CoverageAmount.Value;
                if (request.Deductible.HasValue)
                    policy.Deductible = request.Deductible.Value;
                if (request.Notes != null)
                    policy.Notes = request.Notes;
                
                policy.LastModifiedDate = DateTime.UtcNow;

                _context.Entry(policy).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("The policy was modified by another user. Please refresh and try again.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the policy: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PolicyDto>> GetPolicy(int id)
        {
            var policy = await _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.Agent)
                .Where(p => p.Id == id)
                .Select(p => new PolicyDto
                {
                    Id = p.Id,
                    PolicyNumber = p.PolicyNumber,
                    CustomerId = p.CustomerId,
                    CustomerName = $"{p.Customer.FirstName} {p.Customer.LastName}",
                    AgentId = p.AgentId,
                    AgentName = $"{p.Agent.FirstName} {p.Agent.LastName}",
                    PolicyType = p.PolicyType,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status,
                    PremiumAmount = p.PremiumAmount,
                    CoverageAmount = p.CoverageAmount,
                    Deductible = p.Deductible,
                    Notes = p.Notes,
                    CreatedDate = p.CreatedDate,
                    LastModifiedDate = p.LastModifiedDate
                })
                .FirstOrDefaultAsync();

            if (policy == null)
                return NotFound($"Policy with ID {id} not found");

            return Ok(policy);
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> PatchPolicy(int id, UpdatePolicyDto request)
        {
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            if (userRole != "Admin" && userRole != "Agent")
                return Forbid("Only Admin and Agent users can update policies");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var policy = await _context.Policies.FindAsync(id);
                if (policy == null)
                    return NotFound($"Policy with ID {id} not found");

                if (!string.IsNullOrEmpty(request.Status))
                    policy.Status = request.Status;
                if (request.PremiumAmount.HasValue)
                    policy.PremiumAmount = request.PremiumAmount.Value;
                if (request.CoverageAmount.HasValue)
                    policy.CoverageAmount = request.CoverageAmount.Value;
                if (request.Deductible.HasValue)
                    policy.Deductible = request.Deductible.Value;
                if (request.Notes != null)
                    policy.Notes = request.Notes;
                
                policy.LastModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Policy was modified by another user. Please refresh and try again.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating policy: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            if (userRole != "Admin")
                return Forbid("Only Admin users can delete policies");

            try
            {
                var policy = await _context.Policies.FindAsync(id);
                if (policy == null)
                    return NotFound($"Policy with ID {id} not found");

                _context.Policies.Remove(policy);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting policy: {ex.Message}");
            }
        }


    }
}