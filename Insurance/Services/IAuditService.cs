using InsuranceManagement.Models;

namespace InsuranceManagement.Services
{
    public interface IAuditService
    {
        Task LogAsync(int userId, string action, string entityType, int entityId, string? oldValues, string? newValues, string? ipAddress = null, string? userAgent = null);
        Task<IEnumerable<AuditLog>> GetAuditLogsAsync(int? userId = null, string? entityType = null, int? entityId = null, DateTime? fromDate = null, DateTime? toDate = null);
    }
}