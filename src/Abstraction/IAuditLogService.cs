
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.AuditLog;

namespace AdminDashboard.src.Abstraction
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync();
    }
}