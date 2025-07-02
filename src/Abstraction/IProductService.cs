using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Product;

namespace AdminDashboard.src.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<ProductDto> CreateProductAsync(ProductCreateDto product);
        Task<ProductDto> UpdateProductAsync(Guid id, ProductUpdateDto product);
        Task<ProductDto> DeleteProductAsync(Guid id);
    }
}