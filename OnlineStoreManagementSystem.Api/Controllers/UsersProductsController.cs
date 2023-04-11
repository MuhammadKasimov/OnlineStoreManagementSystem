using Microsoft.AspNetCore.Mvc;
using OnlineStoreManagementSystem.Service.DTOs.Products;
using OnlineStoreManagementSystem.Service.DTOs.Users;
using OnlineStoreManagementSystem.Service.Interfaces.Users;
using OnlineStoreManagementSystem.Service.Services.Products;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Api.Controllers
{
    public class UsersProductsController : BaseController
    {
        private readonly IUserProductService userProductService;

        public UsersProductsController(IUserProductService userProductService)
        {
            this.userProductService = userProductService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync([FromForm] UserProductForCreationDTO dto)
            => Ok(await userProductService.CreateAsync(dto));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await userProductService.GetAllAsync());

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync(long id)
            => Ok(await userProductService.GetAsync(p => p.Id == id));

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
        => Ok(await userProductService.DeleteAsync(id));
    }
}
