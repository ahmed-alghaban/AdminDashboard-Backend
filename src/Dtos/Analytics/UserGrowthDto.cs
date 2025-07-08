using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Dtos.Analytics
{
    public class UserGrowthDto
{
    public DateTime Date { get; set; } // Grouped by day/week/month
    public int Count { get; set; }     // Number of new users
}
}