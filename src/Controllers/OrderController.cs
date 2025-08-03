using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Order;
using AdminDashboard.src.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAllOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10){
            try{
                var paginationResult = await _orderService.GetAllOrdersAsync(pageNumber, pageSize);
                var result = new ApiResult<PaginationResult<OrderDto>>(paginationResult, true, "Orders fetched successfully");
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetOrderById(Guid id){
            try{
                var order = await _orderService.GetOrderByIdAsync(id);
                var result = new ApiResult<OrderDto>(order, true, "Order fetched successfully");
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateOrder(OrderCreateDto order){
            try{
                var newOrder = await _orderService.CreateOrderAsync(order);
                var result = new ApiResult<OrderDto>(newOrder, true, "Order created successfully");
                return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, result);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, OrderStatus status){
            try{
                var updatedOrder = await _orderService.UpdateOrderStatusAsync(id, status);
                var result = new ApiResult<OrderDto>(updatedOrder, true, "Order status updated successfully");
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}