using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStoreManagementSystem.Data.IRepositories;
using OnlineStoreManagementSystem.Domain.Entitties.Carts;
using OnlineStoreManagementSystem.Domain.Entitties.Products;
using OnlineStoreManagementSystem.Domain.Entitties.Users;
using OnlineStoreManagementSystem.Service.DTOs.Users;
using OnlineStoreManagementSystem.Service.Exceptions;
using OnlineStoreManagementSystem.Service.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.Services.Users
{
    public class UserProductService : IUserProductService
    {
        private readonly IGenericRepository<Cart> cartRepository;
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<UserProduct> userProductRepository;
        private readonly IMapper mapper;

        public UserProductService(IGenericRepository<Cart> cartRepository,
            IGenericRepository<Product> productRepository,
            IGenericRepository<UserProduct> userProductRepository,
            IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.userProductRepository = userProductRepository;
            this.mapper = mapper;
        }

        public async ValueTask<UserProduct> CreateAsync(UserProductForCreationDTO dto)
        {
            var existCart = await cartRepository.GetAsync(c => c.Id == dto.CartId);
            if (existCart == null)
                throw new HttpStatusCodeException(404,"Cart not found");

            var existProduct = await productRepository.GetAsync(p => p.Id == dto.ProductId);
            if (existProduct == null)
                throw new HttpStatusCodeException(404, "Product not found");

            existCart.TotalPrice += existProduct.Price * dto.NumberOfProducts;

            cartRepository.Update(existCart);
            var createdUserProduct = await userProductRepository.CreateAsync(mapper.Map<UserProduct>(dto));
            await userProductRepository.SaveChangesAsync();

            return createdUserProduct;
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var isDeleted = await userProductRepository.DeleteAsync(id);

            if (!isDeleted)
                throw new HttpStatusCodeException(404, "Product not found");

            await userProductRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<UserProduct>> GetAllAsync(Expression<Func<UserProduct, bool>> expression = null)
        {
            var userProducts = userProductRepository.GetAll(expression: expression, isTracking: false);

            return await userProducts.ToListAsync();
        }

        public async ValueTask<UserProduct> GetAsync(Expression<Func<UserProduct, bool>> expression)
        {
            var userProduct = await userProductRepository.GetAsync(expression);

            if (userProduct is null)
                throw new HttpStatusCodeException(404, "User not found");

            return userProduct;
        }
    }
}
