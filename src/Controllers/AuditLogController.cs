using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Dtos.AuditLog;
using AdminDashboard.src.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/audit-logs")]
    [Authorize(Roles = "Admin")]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;
        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuditLogs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try{
                var paginationResult = await _auditLogService.GetAllAuditLogsAsync(pageNumber, pageSize);
                var result = new ApiResult<PaginationResult<AuditLogDto>>(paginationResult, true, "Audit logs fetched successfully");
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}