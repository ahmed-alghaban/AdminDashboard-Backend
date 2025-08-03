using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Dtos.Setting;
using AdminDashboard.src.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/settings")]
    [Authorize(Roles = "Admin")]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService){
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSettings(){
            try{
                var settings = await _settingService.GetAllSettingsAsync();
                var result = new ApiResult<IEnumerable<SettingDto>>(settings, true, "Settings fetched successfully");
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSetting(Guid id, SettingUpdateDto setting){
            try{
                var updatedSetting = await _settingService.UpdateSettingAsync(id, setting);
                var result = new ApiResult<SettingDto>(updatedSetting, true, "Setting updated successfully");
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}