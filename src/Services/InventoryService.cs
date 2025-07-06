using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Inventory;
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

        public async Task<IEnumerable<InventoryDto>> GetAllInventoriesAsync()
        {
            var inventories = await _context.Inventories.ToListAsync();
            return _mapper.Map<IEnumerable<InventoryDto>>(inventories);
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