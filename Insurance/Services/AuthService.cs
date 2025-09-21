using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InsuranceManagement.Data;
using InsuranceManagement.Models;
using InsuranceManagement.DTOs;

namespace InsuranceManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly InsuranceDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAuditService _auditService;

        public AuthService(InsuranceDbContext context, IConfiguration configuration, IAuditService auditService)
        {
            _context = context;
            _configuration = configuration;
            _auditService = auditService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.IsActive);

            if (user == null || user.PasswordHash != loginDto.Password)
            {
                await Task.Delay(100);
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            user.LastLoginDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            await _auditService.LogAsync(user.Id, "Login", "User", user.Id, null, null);

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                User = MapToUserDto(user),
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            var customerRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer");
            if (customerRole == null)
            {
                throw new InvalidOperationException("Customer role not found");
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = registerDto.Password,
                RoleId = customerRole.Id,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user = await _context.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstAsync(u => u.Id == user.Id);

            await _auditService.LogAsync(user.Id, "Create", "User", user.Id, null, null);

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                User = MapToUserDto(user),
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        public async Task<AuthResponseDto> RegisterWithRoleAsync(RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == registerDto.RoleId);
            if (role == null)
            {
                throw new InvalidOperationException("Selected role not found");
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = registerDto.Password,
                RoleId = registerDto.RoleId,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user = await _context.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstAsync(u => u.Id == user.Id);

            await _auditService.LogAsync(user.Id, "Create", "User", user.Id, null, null);

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                User = MapToUserDto(user),
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            await _auditService.LogAsync(userId, "Logout", "User", userId, null, null);
            return true;
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException("Refresh token functionality not implemented");
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            if (user.PasswordHash != changePasswordDto.CurrentPassword)
            {
                throw new UnauthorizedAccessException("Current password is incorrect");
            }

            user.PasswordHash = changePasswordDto.NewPassword;
            await _context.SaveChangesAsync();

            await _auditService.LogAsync(userId, "Update", "User", userId, null, "Password changed");

            return true;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = _configuration["Jwt:Key"];

                if (string.IsNullOrEmpty(key))
                    return Task.FromResult(false);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("JWT key not configured");

            var claims = new List<System.Security.Claims.Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Role, user.Role.Name)
            };

            foreach (var rolePermission in user.Role.RolePermissions)
            {
                claims.Add(new System.Security.Claims.Claim("permission", rolePermission.Permission.Name));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = new RoleDto
                {
                    Id = user.Role.Id,
                    Name = user.Role.Name,
                    Permissions = user.Role.RolePermissions.Select(rp => new PermissionDto
                    {
                        Id = rp.Permission.Id,
                        Name = rp.Permission.Name,
                        Resource = rp.Permission.Resource,
                        Action = rp.Permission.Action
                    }).ToList()
                },
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
                LastLoginDate = user.LastLoginDate
            };
        }
    }
}