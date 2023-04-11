using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreManagementSystem.Api.Helpers;
using OnlineStoreManagementSystem.Service.DTOs.Products;
using OnlineStoreManagementSystem.Service.DTOs.Users;
using OnlineStoreManagementSystem.Service.Interfaces.Users;
using OnlineStoreManagementSystem.Service.Services.Products;
using System.Data;
using System.Security.Cryptography.X509Certificates;
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

        [HttpPost, Authorize(Roles = CustomRoles.UserRole)]
        public async ValueTask<IActionResult> CreateAsync([FromForm] UserProductForCreationDTO dto)
            => Ok(await userProductService.CreateAsync(dto));

        [HttpGet("{cartId}/all"), Authorize(Roles = CustomRoles.AdminRole)]
        public async ValueTask<IActionResult> GetAllAsync(long cartId)
            => Ok(await userProductService.GetAllAsync(c => c.CartId == cartId));

        [HttpGet("{id}"), Authorize(Roles = CustomRoles.AdminRole)]
        public async ValueTask<IActionResult> GetAsync(long id)
            => Ok(await userProductService.GetAsync(p => p.Id == id));

        [HttpDelete("{id}"), Authorize(Roles = CustomRoles.AdminRole)]
        public async ValueTask<IActionResult> DeleteAsync(long id)
        => Ok(await userProductService.DeleteAsync(id));
    }
}
