using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Dtos.Setting
{
    public class SettingUpdateDto
    {
        public string Value { get; set; } = string.Empty;
        public string Category { get; set; } = "General";
    }
}