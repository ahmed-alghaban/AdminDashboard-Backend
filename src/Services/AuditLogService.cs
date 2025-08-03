using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.AuditLog;
using AdminDashboard.src.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.src.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public AuditLogService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResult<AuditLogDto>> GetAllAuditLogsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var auditLogs = await _context.AuditLogs.ToListAsync();
            var mappedAuditLogs = _mapper.Map<List<AuditLogDto>>(auditLogs);
            return await PaginationSearch.PaginationAsync(mappedAuditLogs, pageNumber, pageSize);
        }
    }
}