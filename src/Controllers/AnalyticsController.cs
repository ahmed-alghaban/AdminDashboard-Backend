using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService){
            _analyticsService = analyticsService;
        }

        [HttpGet("sales-summary")]
        public async Task<IActionResult> GetSalesSummary(string timeframe = "daily"){
            try{
                var sales = await _analyticsService.GetSalesSummaryAsync(timeframe);
                return Ok(sales);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}