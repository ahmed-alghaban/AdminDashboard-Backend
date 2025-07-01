using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Entities
{
    public class Product
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; }
    public string SKU { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string? ImageUrl { get; set; }
    

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public ICollection<OrderItem> OrderItems { get; set; }
    public Inventory Inventory { get; set; }
}

}