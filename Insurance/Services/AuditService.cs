using Microsoft.EntityFrameworkCore;
using InsuranceManagement.Data;
using InsuranceManagement.Models;

namespace InsuranceManagement.Services
{
    public class AuditService : IAuditService
    {
        private readonly InsuranceDbContext _context;

        public AuditService(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(int userId, string action, string entityType, int entityId, string? oldValues, string? newValues, string? ipAddress = null, string? userAgent = null)
        {
            var auditLog = new AuditLog
            {
                UserId = userId,
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                OldValues = oldValues,
                NewValues = newValues,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync(int? userId = null, string? entityType = null, int? entityId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = _context.AuditLogs
                .Include(a => a.User)
                .AsQueryable();

            if (userId.HasValue)
                query = query.Where(a => a.UserId == userId.Value);

            if (!string.IsNullOrEmpty(entityType))
                query = query.Where(a => a.EntityType == entityType);

            if (entityId.HasValue)
                query = query.Where(a => a.EntityId == entityId.Value);

            if (fromDate.HasValue)
                query = query.Where(a => a.Timestamp >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(a => a.Timestamp <= toDate.Value);

            return await query
                .OrderByDescending(a => a.Timestamp)
                .Take(1000)
                .ToListAsync();
        }
    }
}