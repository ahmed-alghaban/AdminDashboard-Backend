using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Inventory;

namespace AdminDashboard.src.Abstraction
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDto>> GetAllInventoriesAsync();
        Task<InventoryDto> UpdateQuantityAsync(Guid id, int quantity);
    }
}