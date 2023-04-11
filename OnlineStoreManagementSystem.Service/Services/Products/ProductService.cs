using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStoreManagementSystem.Data.IRepositories;
using OnlineStoreManagementSystem.Domain.Entitties.Products;
using OnlineStoreManagementSystem.Service.DTOs.Products;
using OnlineStoreManagementSystem.Service.Exceptions;
using OnlineStoreManagementSystem.Service.Extensions;
using OnlineStoreManagementSystem.Service.Interfaces.Attachments;
using OnlineStoreManagementSystem.Service.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IAttachmentService attachmentService;
        private readonly IMapper mapper;

        public ProductService(IGenericRepository<Product> productRepository,
            IAttachmentService attachmentService,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.attachmentService = attachmentService;
            this.mapper = mapper;
        }

        public async ValueTask<Product> CreateAsync(ProductForCreationDTO dto)
        {
            var createdProduct = await productRepository.CreateAsync(mapper.Map<Product>(dto));

            var createdAttachment = await attachmentService.UploadAsync(dto.FormFile.ToAttachmentOrDefault());
            createdProduct.AttachmentId = createdAttachment.Id;

            await productRepository.SaveChangesAsync();

            return createdProduct;
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var isDeleted = await productRepository.DeleteAsync(id);

            if (!isDeleted)
                throw new HttpStatusCodeException(404, "User not found");

            await productRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> expression = null)
        {
            var products = productRepository.GetAll(expression: expression, isTracking: false);

            return await products.ToListAsync();

        }

        public async ValueTask<Product> GetAsync(Expression<Func<Product, bool>> expression)
        {
            var product = await productRepository.GetAsync(expression);

            if (product is null)
                throw new HttpStatusCodeException(404, "Product not found");

            return product;
        }

        public async ValueTask<Product> UpdateAsync(long id, ProductForCreationDTO dto)
        {
            var existProduct = await productRepository.GetAsync(
                u => u.Id == id);

            if (existProduct == null)
                throw new HttpStatusCodeException(404, "Product not found");

            await attachmentService.UpdateAsync(existProduct.AttachmentId,dto.FormFile.ToAttachmentOrDefault().Stream);

            existProduct.UpdatedAt = DateTime.UtcNow;
            existProduct = productRepository.Update(mapper.Map(dto, existProduct));
            await productRepository.SaveChangesAsync();

            return existProduct;
        }
    }
}
