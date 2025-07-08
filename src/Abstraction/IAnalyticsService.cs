using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Analytics;

namespace AdminDashboard.src.Abstraction
{
    public interface IAnalyticsService
    {
        Task<IEnumerable<SalesSummaryDto>> GetSalesSummaryAsync(string timeframe, DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<TopProductDto>> GetTopProductsAsync(int limit = 5, string sortBy = "quantity");
        Task<IEnumerable<UserGrowthDto>> GetUsersGrowthAsync(int limit = 5, string sortBy = "count");
        Task<IEnumerable<OrderStatusSummaryDto>> GetOrderStatusSummaryAsync();
    }
}