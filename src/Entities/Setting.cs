using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Entities
{
   public class Setting
{
    public Guid SettingId { get; set; }

    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;

    public string Category { get; set; } = "General";
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid? UpdatedBy { get; set; } // Admin who changed it
}
}