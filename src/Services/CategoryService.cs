using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Configs.Exceptions;
using AdminDashboard.src.Dtos.Category;
using AdminDashboard.src.Entities;
using AdminDashboard.src.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.src.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id) ?? throw new Exception("Category not found");
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto category)
        {
            await _context.EnsureUniqueAsync<Category>(c => c.Name == category.Name, "Category already exists");
            var newCategory = _mapper.Map<Category>(category);
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(newCategory);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(Guid id, CategoryUpdateDto category)
        {
            var existingCategory = await _context.Categories.FindAsync(id) ?? throw new Exception("Category not found");
            _context.Update(_mapper.Map(category, existingCategory));
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(existingCategory);
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id) ?? throw new Exception("Category not found");
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}