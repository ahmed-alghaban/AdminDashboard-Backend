using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Dtos.Product
{
    public class ProductUpdateDto
    {
        [StringLength(100)]
        public string? ProductName { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [StringLength(50)]
        public string? SKU { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        [StringLength(2048)]
        public string? ImageUrl { get; set; }
    }
}