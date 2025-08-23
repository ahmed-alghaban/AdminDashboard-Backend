using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Order;
using AdminDashboard.src.Entities;
using AdminDashboard.src.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.src.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PaginationResult<OrderDto>> GetAllOrdersAsync(int pageNumber = 1, int pageSize = 10)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(item => item.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            var mappedOrders = _mapper.Map<List<OrderDto>>(orders);
            return await PaginationSearch.PaginationAsync(mappedOrders, pageNumber, pageSize);
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id)
                ?? throw new KeyNotFoundException("Order not found");
            return _mapper.Map<OrderDto>(order);
        }
        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto order)
        {
            var newOrder = _mapper.Map<Order>(order);
            decimal totalAmount = 0;
            newOrder.UserId = GetUserIDFromToken.GetCurrentUserId(_httpContextAccessor);

            // Process each order item and set UnitPrice
            foreach (var orderItem in newOrder.OrderItems)
            {
                var product = await _context.Products
                    .Include(p => p.Inventory)
                    .FirstOrDefaultAsync(p => p.ProductId == orderItem.ProductId)
                    ?? throw new KeyNotFoundException("Product not found");

                if (product.QuantityInStock < orderItem.Quantity)
                {
                    throw new InvalidOperationException("Insufficient stock");
                }

                // Set the UnitPrice on the entity
                orderItem.UnitPrice = product.Price;
                totalAmount += product.Price * orderItem.Quantity;

                // Update inventory
                if (product.Inventory != null)
                {
                    product.Inventory.QuantityAvailable -= orderItem.Quantity;
                }
                else
                {
                    throw new InvalidOperationException($"Inventory not found for product {product.ProductId}");
                }
            }
            newOrder.TotalAmount = totalAmount;
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            var orderObject = await _context.Orders
                .Include(order => order.User)
                .Include(order => order.OrderItems)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(order => order.OrderId == newOrder.OrderId)
                ?? throw new KeyNotFoundException("Order not found");
            return _mapper.Map<OrderDto>(orderObject);
        }

        public async Task<OrderDto> UpdateOrderStatusAsync(Guid id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id) ?? throw new KeyNotFoundException("Order not found");
            order.Status = status;
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }
    }
}