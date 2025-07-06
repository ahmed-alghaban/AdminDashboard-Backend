using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Dtos.Inventory;
using AdminDashboard.src.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService){
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventories(){
            try{
                var inventories = await _inventoryService.GetAllInventoriesAsync();
                var result = new ApiResult<IEnumerable<InventoryDto>>(inventories, true, "Inventories fetched successfully");
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuantity(Guid id, int quantity){
            try{
                var updatedInventory = await _inventoryService.UpdateQuantityAsync(id, quantity);
                var result = new ApiResult<InventoryDto>(updatedInventory, true, "Inventory updated successfully");
                return Ok(result);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}