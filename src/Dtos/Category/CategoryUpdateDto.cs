using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Dtos.Category
{
    public class CategoryUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}