using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Configs;
namespace AdminDashboard.src.Entities
{
    public class AuditLog
    {
        public Guid AuditLogId { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public AuditActionType ActionType { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public Guid? EntityId { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? IpAddress { get; set; }
    public string Description { get; set; } = string.Empty;
    }
}