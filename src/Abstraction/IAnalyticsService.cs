using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Analytics;

namespace AdminDashboard.src.Abstraction
{
    public interface IAnalyticsService
    {
        Task<IEnumerable<SalesSummaryDto>> GetSalesAnalyticsAsync();
        Task<IEnumerable<TopProductDto>> GetBestSellingProductsAsync();
        Task<IEnumerable<UserGrowthDto>> GetUsersGrowthAsync();
        Task<IEnumerable<OrderStatusSummaryDto>> GetOrderStatusDistributionAsync();
    }
}