using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Analytics;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.src.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly AppDbContext _context;
        public AnalyticsService(AppDbContext context){
            _context = context;
        }

    public async Task<IEnumerable<SalesSummaryDto>> GetSalesSummaryAsync(string? timeframe)
    {
        var query = _context.Orders.AsQueryable();

        if (timeframe == "daily")
        {
            return await query
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new SalesSummaryDto
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(o => o.TotalAmount),
                    OrderCount = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }
        else if (timeframe == "weekly")
        {
            return await query
                .GroupBy(o => o.OrderDate.Date.AddDays(-((int)o.OrderDate.DayOfWeek == 0 ? 6 : (int)o.OrderDate.DayOfWeek - 1)))
                .Select(g => new SalesSummaryDto
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(o => o.TotalAmount),
                    OrderCount = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }
        else if (timeframe == "monthly")
        {
            return await query
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new SalesSummaryDto
                {
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1, 0, 0, 0, DateTimeKind.Utc),
                    TotalAmount = g.Sum(o => o.TotalAmount),
                    OrderCount = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        throw new ArgumentException("Invalid timeframe. Use 'daily', 'weekly', or 'monthly'.");
    }


        public async Task<IEnumerable<TopProductDto>> GetTopProductsAsync(int limit = 5, string sortBy = "quantity"){
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserGrowthDto>> GetUsersGrowthAsync(int limit = 5, string sortBy = "count"){
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderStatusSummaryDto>> GetOrderStatusSummaryAsync(){
            throw new NotImplementedException();
        }
    }
}