using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Configs;

namespace AdminDashboard.src.Dtos.AuditLog
{
    public class AuditLogDto
    {
        public Guid AuditLogId { get; set; }
        public Guid UserId { get; set; }
        public AuditActionType ActionType { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}