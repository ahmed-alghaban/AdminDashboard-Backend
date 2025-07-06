using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Setting;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.src.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SettingService(AppDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SettingDto>> GetAllSettingsAsync()
        {
            var settings = await _context.Settings.ToListAsync();
            return _mapper.Map<IEnumerable<SettingDto>>(settings);
        }

        public async Task<SettingDto> UpdateSettingAsync(Guid id, SettingUpdateDto setting)
        {
            var existingSetting = await _context.Settings.FindAsync(id) ?? throw new KeyNotFoundException("Setting not found");
            _mapper.Map(setting, existingSetting);
            await _context.SaveChangesAsync();
            return _mapper.Map<SettingDto>(existingSetting);
        }
    }
}