using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Dtos.Product
{
    public class ProductCreateDto
    {
        [Required]
        [StringLength(100)]
        public required string ProductName { get; set; }
        [Required]
        [StringLength(500)]
        public required string Description { get; set; }
        [Required]
        [StringLength(50)]
        public required string SKU { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        [StringLength(2048)]
        public string? ImageUrl { get; set; }
        [Required]
        public Guid InventoryId { get; set; }
    }
}