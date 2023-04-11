using Microsoft.AspNetCore.Mvc;
using OnlineStoreManagementSystem.Service.DTOs.Products;
using OnlineStoreManagementSystem.Service.Interfaces.Products;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Api.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync([FromForm] ProductForCreationDTO dto)
            => Ok(await productService.CreateAsync(dto));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await productService.GetAllAsync());

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync(long id)
            => Ok(await productService.GetAsync(p => p.Id == id));

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
            => Ok(await productService.DeleteAsync(id));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> UpdateAsync(long id, [FromForm] ProductForCreationDTO dto)
            => Ok(await productService.UpdateAsync(id, dto));
    }
}
