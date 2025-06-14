using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Entities
{
    public class Inventory
{
    public Guid InventoryId { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public int QuantityAvailable { get; set; }
    public int ReorderLevel { get; set; }
    public DateTime? LastRestockedAt { get; set; }
}

}