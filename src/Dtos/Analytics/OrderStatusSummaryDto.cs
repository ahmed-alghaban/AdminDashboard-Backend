using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Configs;

namespace AdminDashboard.src.Dtos.Analytics
{
    public class OrderStatusSummaryDto
{
    public OrderStatus Status { get; set; } 
    public int Count { get; set; }
}
}