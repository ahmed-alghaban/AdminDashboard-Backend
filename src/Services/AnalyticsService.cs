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

    public async Task<IEnumerable<SalesSummaryDto>> GetSalesSummaryAsync(DateTime startDate = default, DateTime endDate = default, string timeframe = "daily")
        {
            var query = _context.Orders.AsQueryable();

            if (startDate != default && endDate != default)
            {
                TimeSpan duration = endDate - startDate;
                Console.WriteLine($"Duration in days: {duration.TotalDays}");

                if(duration.TotalDays == 1)
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
                    if(duration.TotalDays == 7)
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
            if(duration.TotalDays == 30)
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
                }
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
            var query = _context.OrderItems.AsQueryable();
            if(sortBy == "quantity")
            {
                query = query.OrderByDescending(o => o.Quantity);
            }
            else if(sortBy == "revenue")
            {
                query = query.OrderByDescending(o => o.Quantity * o.Product.Price);
            }
            return await query
                .Select(o => new TopProductDto
                {
                    ProductId = o.ProductId,
                    ProductName = o.Product.ProductName,
                    QuantitySold = o.Quantity,
                    TotalRevenue = o.Quantity * o.Product.Price,
                })
                .Take(limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserGrowthDto>> GetUsersGrowthAsync(int limit = 5, string sortBy = "count"){
            var query = _context.Users.AsQueryable();
            return await query
                .GroupBy(u => u.CreatedAt.Date) // timestamp
                .Select(g => new UserGrowthDto
                {
                    Date = DateTime.SpecifyKind(g.Key, DateTimeKind.Utc),
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderStatusSummaryDto>> GetOrderStatusSummaryAsync(){
            var query = _context.Orders.AsQueryable();
            return await query
                .GroupBy(o => o.Status)
                .Select(g => new OrderStatusSummaryDto{
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
        }
    }
}