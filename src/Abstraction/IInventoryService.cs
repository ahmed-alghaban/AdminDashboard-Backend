using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Inventory;
using AdminDashboard.src.Utilities;

namespace AdminDashboard.src.Abstraction
{
    public interface IInventoryService
    {
        Task<PaginationResult<InventoryDto>> GetAllInventoriesAsync(int pageNumber = 1, int pageSize = 10);
        Task<InventoryDto> UpdateQuantityAsync(Guid id, int quantity);
    }
}