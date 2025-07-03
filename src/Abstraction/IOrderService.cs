using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Order;

namespace AdminDashboard.src.Abstraction
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(Guid id);
        Task<OrderDto> CreateOrderAsync(OrderCreateDto order);
        Task<OrderDto> UpdateOrderAsync(Guid id, OrderUpdateDto order);
        Task<OrderDto> DeleteOrderAsync(Guid id);

    }
}