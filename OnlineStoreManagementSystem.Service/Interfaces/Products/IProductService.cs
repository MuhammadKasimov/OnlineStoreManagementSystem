using OnlineStoreManagementSystem.Domain.Entitties.Products;
using OnlineStoreManagementSystem.Service.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.Interfaces.Products
{
    public interface IProductService
    {
        ValueTask<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> expression = null);
        ValueTask<Product> GetAsync(Expression<Func<Product, bool>> expression);
        ValueTask<Product> CreateAsync(ProductForCreationDTO dto);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<Product> UpdateAsync(long id, ProductForCreationDTO dto);
    }
}
