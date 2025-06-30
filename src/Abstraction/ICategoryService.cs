using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Category;

namespace AdminDashboard.src.Abstraction
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto category);
        Task<CategoryDto> UpdateCategoryAsync(Guid id, CategoryUpdateDto category);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}