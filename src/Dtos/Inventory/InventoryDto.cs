using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Dtos.Inventory
{
    public class InventoryDto
    {
        public Guid InventoryId { get; set; }
        public Guid ProductId { get; set; }
        public int QuantityAvailable { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime? LastRestockedAt { get; set; }
        
    }
}