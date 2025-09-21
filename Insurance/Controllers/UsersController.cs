using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsuranceManagement.Data;
using InsuranceManagement.DTOs;

namespace InsuranceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly InsuranceDbContext _context;

        public UsersController(InsuranceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = new RoleDto { Id = u.Role.Id, Name = u.Role.Name },
                    IsActive = u.IsActive,
                    CreatedDate = u.CreatedDate,
                    AgentId = u.AgentId
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("agents")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAgents()
        {
            var agents = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role.Name == "Agent")
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = new RoleDto { Id = u.Role.Id, Name = u.Role.Name }
                })
                .ToListAsync();

            return Ok(agents);
        }

        [HttpGet("customers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetCustomers()
        {
            var customers = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role.Name == "Customer")
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = new RoleDto { Id = u.Role.Id, Name = u.Role.Name },
                    AgentId = u.AgentId
                })
                .ToListAsync();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Id == id)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = new RoleDto { Id = u.Role.Id, Name = u.Role.Name },
                    IsActive = u.IsActive,
                    CreatedDate = u.CreatedDate,
                    AgentId = u.AgentId
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            if (updateDto.AgentId.HasValue)
                user.AgentId = updateDto.AgentId.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}