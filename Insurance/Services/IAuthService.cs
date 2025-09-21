using InsuranceManagement.Models;
using InsuranceManagement.DTOs;

namespace InsuranceManagement.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> RegisterWithRoleAsync(RegisterDto registerDto);
        Task<bool> LogoutAsync(int userId);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
        Task<User?> GetUserByIdAsync(int userId);
        Task<bool> ValidateTokenAsync(string token);
    }
}