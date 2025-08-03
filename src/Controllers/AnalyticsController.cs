using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/analytics")]
    [Authorize(Roles = "Admin,Manager")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService){
            _analyticsService = analyticsService;
        }

        [HttpGet("sales-summary")]
        public async Task<IActionResult> GetSalesSummary(DateTime startDate, DateTime endDate, string timeframe = "daily"){
            try{
                var sales = await _analyticsService.GetSalesSummaryAsync( startDate, endDate , timeframe);
                var response = new {
                    data = sales,
                    message = "Sales summary fetched successfully"
                };
                return Ok(response);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("top-products")]
        public async Task<IActionResult> GetTopProducts(int limit = 5, string sortBy = "quantity"){
            try{
                var products = await _analyticsService.GetTopProductsAsync(limit, sortBy);
                var response = new {
                    data = products,
                    message = "Top products fetched successfully"
                };
                return Ok(response);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users-growth")]
        public async Task<IActionResult> GetUsersGrowth(int limit = 5, string sortBy = "count"){
            try{
                var users = await _analyticsService.GetUsersGrowthAsync(limit, sortBy);
                var response = new {
                    data = users,
                    message = "Users growth fetched successfully"
                };
                return Ok(response);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("order-status-summary")]
        public async Task<IActionResult> GetOrderStatusSummary(){
            try{
                var statuses = await _analyticsService.GetOrderStatusSummaryAsync();
                var response = new {
                    data = statuses,
                    message = "Order status summary fetched successfully"
                };
                return Ok(response);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}