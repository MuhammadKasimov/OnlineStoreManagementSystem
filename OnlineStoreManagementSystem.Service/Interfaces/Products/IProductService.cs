using OnlineStoreManagementSystem.Domain.Entitties.Products;
using OnlineStoreManagementSystem.Domain.Entitties.Users;
using OnlineStoreManagementSystem.Service.DTOs.Products;
using OnlineStoreManagementSystem.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.Interfaces.Products
{
    public interface IProductService
    {
        ValueTask<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> expression = null);
        ValueTask<Product> GetAsync(Expression<Func<Product, bool>> expression);
        ValueTask<Product> CreateAsync(ProductForCreationDTO dto);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<Product> UpdateAsync(int id, ProductForCreationDTO dto);
    }
}
