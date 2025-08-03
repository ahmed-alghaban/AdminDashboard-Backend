using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Order;
using AdminDashboard.src.Utilities;

namespace AdminDashboard.src.Abstraction
{
    public interface IOrderService
    {
        Task<PaginationResult<OrderDto>> GetAllOrdersAsync(int pageNumber = 1, int pageSize = 10);
        Task<OrderDto> GetOrderByIdAsync(Guid id);
        Task<OrderDto> CreateOrderAsync(OrderCreateDto order);
        Task<OrderDto> UpdateOrderStatusAsync(Guid id, OrderStatus status);
    }
}