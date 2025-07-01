using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Dtos.Category;
using AdminDashboard.src.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                var result = new ApiResult<IEnumerable<CategoryDto>>(categories, true, "Categories fetched successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                var result = new ApiResult<CategoryDto>(category, true, "Category fetched successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto category)
        {
            try
            {
                var newCategory = await _categoryService.CreateCategoryAsync(category);
                var result = new ApiResult<CategoryDto>(newCategory, true, "Category created successfully");
                return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.CategoryId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryUpdateDto category)
        {
            try
            {
                var updatedCategory = await _categoryService.UpdateCategoryAsync(id, category);
                var result = new ApiResult<CategoryDto>(updatedCategory, true, "Category updated successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                var isDeleted = await _categoryService.DeleteCategoryAsync(id);
                var result = new ApiResult<bool>(isDeleted, true, "Category deleted successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}