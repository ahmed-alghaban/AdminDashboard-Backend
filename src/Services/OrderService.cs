using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Order;
using AdminDashboard.src.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.src.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public OrderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(item => item.Product)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
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
            
            // Process each order item and set UnitPrice
            foreach (var orderItem in newOrder.OrderItems)
            {
                var product = await _context.Products
                    .Include(p => p.Inventory)
                    .FirstOrDefaultAsync(p => p.ProductId == orderItem.ProductId) 
                    ?? throw new KeyNotFoundException("Product not found");
                
                if(product.QuantityInStock < orderItem.Quantity){
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

        public async Task<OrderDto> UpdateOrderAsync(Guid id, OrderUpdateDto order){
            throw new NotImplementedException();
        }
        public async Task<OrderDto> DeleteOrderAsync(Guid id){
            throw new NotImplementedException();
        }
    }
}