using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Category;
using AdminDashboard.src.Utilities;

namespace AdminDashboard.src.Abstraction
{
    public interface ICategoryService
    {
        Task<PaginationResult<CategoryDto>> GetAllCategoriesAsync(int pageNumber = 1, int pageSize = 10);
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto category);
        Task<CategoryDto> UpdateCategoryAsync(Guid id, CategoryUpdateDto category);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}