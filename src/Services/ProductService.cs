using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Product;
using AdminDashboard.src.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.src.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id) ?? throw new KeyNotFoundException("Product not found");
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(ProductCreateDto product)
        {
            var newProduct = _mapper.Map<Product>(product);
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(newProduct);
        }

        public async Task<ProductDto> UpdateProductAsync(Guid id, ProductUpdateDto product)
        {
            var existingProduct = await _context.Products.FindAsync(id) ?? throw new KeyNotFoundException("Product not found");
            _mapper.Map(product, existingProduct);
            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(existingProduct);
        }

        public async Task<ProductDto> DeleteProductAsync(Guid id)
        {
            var existingProduct = await _context.Products.FindAsync(id) ?? throw new KeyNotFoundException("Product not found");
            existingProduct.IsActive = false;
            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(existingProduct);
        }
    }
}