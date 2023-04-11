using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreManagementSystem.Api.Helpers;
using OnlineStoreManagementSystem.Service.DTOs.Users;
using OnlineStoreManagementSystem.Service.Interfaces.Users;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userForUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize(Roles = CustomRoles.UserRole)]
        public async ValueTask<IActionResult> UpdateAsync([FromRoute] int id, UserForUpdateDTO userForUpdateDTO)
            => Ok(await userService.UpdateAsync(id, userForUpdateDTO));

        /// <summary>
        /// GetAll all users
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = CustomRoles.AdminRole)]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await userService.GetAllAsync());

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/admin"), Authorize(Roles = CustomRoles.AdminRole)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
            => Ok(await userService.GetAsync(u => u.Id == id));

        [HttpGet("user")]
        public async ValueTask<IActionResult> GetAsync()
            => Ok(await userService.GetUserInfoAsync());

        /// <summary>
        /// User Change Password
        /// </summary>
        /// <param name="userForChangePasswordDTO"></param>
        /// <returns></returns>
        [HttpPatch("password"), Authorize(Roles = CustomRoles.UserRole)]
        public async ValueTask<IActionResult> ChangePasswordAsync(UserForChangePasswordDTO userForChangePasswordDTO)
            => Ok(await userService.ChangePasswordAsync(userForChangePasswordDTO));


        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize(Roles = CustomRoles.UserRole)]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await userService.DeleteAsync(id));
    }
}
