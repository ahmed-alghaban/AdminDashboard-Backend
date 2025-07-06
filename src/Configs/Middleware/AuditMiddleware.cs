using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.AuditLog;
using AdminDashboard.src.Entities;
using AutoMapper;

namespace AdminDashboard.src.Configs.Middleware
{
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuditMiddleware> _logger;
        public AuditMiddleware(RequestDelegate next, ILogger<AuditMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, AppDbContext dbContext, IMapper mapper)
        {
            var request = context.Request;
            var method = request.Method;
            var path = request.Path.Value?.ToLower();
            
            // Check if this is a login endpoint
            var isLoginEndpoint = path?.Contains("/auth/") == true || 
                                path?.Contains("/login") == true;
            
            if (method == "POST" || method == "PUT" || method == "DELETE")
            {
                var userIdStr = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                // For login endpoints, allow unauthenticated requests and track them
                if (isLoginEndpoint && string.IsNullOrEmpty(userIdStr))
                {
                    // Use a system user ID for login attempts
                    // You need to create a system user in your database with this ID
                    var systemUserId = Guid.Parse("00000000-0000-0000-0000-000000000000");
                    
                    var ipAddress = context.Connection.RemoteIpAddress?.ToString();
                    var auditLog = new AuditLogCreateDto
                    {
                        UserId = systemUserId,
                        ActionType = AuditActionType.Login,
                        EntityName = "auth",
                        Timestamp = DateTime.UtcNow,
                        IpAddress = ipAddress,
                        Description = $"{method} {request.Path} - Login attempt"
                    };
                    
                    var auditLogEntity = mapper.Map<AuditLog>(auditLog);
                    await dbContext.AuditLogs.AddAsync(auditLogEntity);
                    await dbContext.SaveChangesAsync();
                    
                    await _next(context);
                    return;
                }
                
                // For non-login endpoints, require authentication
                if (!isLoginEndpoint && (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var parsedUserId)))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Authentication required for this action.");
                    return;
                }
                
                // Only audit if we have a valid user ID
                if (!string.IsNullOrEmpty(userIdStr) && Guid.TryParse(userIdStr, out var userId))
                {
                    var ipAddress = context.Connection.RemoteIpAddress?.ToString();
                    var entityName = GetEntityNameFromPath(request.Path);
                    var auditLog = new AuditLogCreateDto
                    {
                        UserId = userId,
                        ActionType = method == "POST" ? AuditActionType.Create : method == "PUT" ? AuditActionType.Update : AuditActionType.Delete,
                        EntityName = entityName,
                        Timestamp = DateTime.UtcNow,
                        IpAddress = ipAddress,
                        Description = $"{method} {request.Path}"
                    };
                    var auditLogEntity = mapper.Map<AuditLog>(auditLog);
                    await dbContext.AuditLogs.AddAsync(auditLogEntity);
                    await dbContext.SaveChangesAsync();
                }
            }
            await _next(context);
        }
        private string GetEntityNameFromPath(PathString path)
        {
            var segments = path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length >= 2)
                return segments[2];
            return "unknown";
        }
    }
}