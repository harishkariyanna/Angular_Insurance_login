using Microsoft.AspNetCore.Mvc;
using InsuranceManagement.Services;
using InsuranceManagement.DTOs;

namespace InsuranceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest("Email and password are required");
                }

                var loginDto = new LoginDto
                {
                    Email = request.Email,
                    Password = request.Password
                };

                var response = await _authService.LoginAsync(loginDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Request is null");

                var registerDto = new RegisterDto
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Password = request.Password,
                    ConfirmPassword = request.Password,
                    RoleId = request.RoleId
                };

                var response = await _authService.RegisterWithRoleAsync(registerDto);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message, stackTrace = ex.StackTrace });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Registration failed", details = ex.Message, stackTrace = ex.StackTrace, innerException = ex.InnerException?.Message });
            }
        }

        [HttpGet("test")]
        public async Task<ActionResult> TestDatabase()
        {
            try
            {
                var user = await _authService.GetUserByIdAsync(1);
                return Ok(new { message = "Database connection working", hasUsers = user != null });
            }
            catch (Exception ex)
            {
                return Ok(new { message = "Database error", error = ex.Message });
            }
        }
    }
}