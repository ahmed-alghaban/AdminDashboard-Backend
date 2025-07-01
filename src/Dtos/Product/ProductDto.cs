using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Category;

namespace AdminDashboard.src.Dtos.Product
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}