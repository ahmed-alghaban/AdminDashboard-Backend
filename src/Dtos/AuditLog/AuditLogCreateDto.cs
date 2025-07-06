using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Configs;

namespace AdminDashboard.src.Dtos.AuditLog
{
    public class AuditLogCreateDto
    {
        public Guid UserId { get; set; }
        public AuditActionType ActionType { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? IpAddress { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}