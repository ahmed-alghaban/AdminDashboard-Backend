using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Dtos.Order;
using AdminDashboard.src.Utilities;
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
        public async Task<IActionResult> GetAllOrders(){
            try{
                var orders = await _orderService.GetAllOrdersAsync();
                var result = new ApiResult<IEnumerable<OrderDto>>(orders, true, "Orders fetched successfully");
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
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
        
    }
}