
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.AuditLog;
using AdminDashboard.src.Utilities;

namespace AdminDashboard.src.Abstraction
{
    public interface IAuditLogService
    {
        Task<PaginationResult<AuditLogDto>> GetAllAuditLogsAsync(int pageNumber = 1, int pageSize = 10);
    }
}