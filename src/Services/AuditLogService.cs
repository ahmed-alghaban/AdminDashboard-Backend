using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.AuditLog;
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

        public async Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync()
        {
            var auditLogs = await _context.AuditLogs.ToListAsync();
            return _mapper.Map<IEnumerable<AuditLogDto>>(auditLogs);
        }
    }
}