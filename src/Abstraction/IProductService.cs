using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Product;
using AdminDashboard.src.Utilities;

namespace AdminDashboard.src.Abstraction
{
    public interface IProductService
    {
        Task<PaginationResult<ProductDto>> GetAllProductsAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<ProductDto> CreateProductAsync(ProductCreateDto product);
        Task<ProductDto> UpdateProductAsync(Guid id, ProductUpdateDto product);
        Task<ProductDto> DeleteProductAsync(Guid id);
    }
}