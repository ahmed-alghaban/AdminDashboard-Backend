using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Dtos.Analytics
{
    public class SalesSummaryDto
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderCount { get; set; }
    }
}