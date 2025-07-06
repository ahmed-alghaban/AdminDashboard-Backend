using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Setting;

namespace AdminDashboard.src.Abstraction
{
    public interface ISettingService
    {
        Task<IEnumerable<SettingDto>> GetAllSettingsAsync();
        Task<SettingDto> UpdateSettingAsync(Guid id, SettingUpdateDto setting);
    }
}