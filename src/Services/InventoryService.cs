using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Inventory;
using AdminDashboard.src.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.src.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InventoryService(AppDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResult<InventoryDto>> GetAllInventoriesAsync(int pageNumber = 1, int pageSize = 10)
        {
            var inventories = await _context.Inventories.ToListAsync();
            var mappedInventories = _mapper.Map<List<InventoryDto>>(inventories);
            return await PaginationSearch.PaginationAsync(mappedInventories, pageNumber, pageSize);
        }

        public async Task<InventoryDto> UpdateQuantityAsync(Guid id, int quantity)
        {
            var inventory = await _context.Inventories.FindAsync(id) ?? throw new KeyNotFoundException("Inventory not found");
            inventory.QuantityAvailable = quantity;
            await _context.SaveChangesAsync();
            return _mapper.Map<InventoryDto>(inventory);
        }
    }
}