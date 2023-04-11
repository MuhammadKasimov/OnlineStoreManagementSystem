using OnlineStoreManagementSystem.Domain.Entitties.Users;
using OnlineStoreManagementSystem.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.Interfaces.Users
{
    public interface IUserService
    {
        ValueTask<IEnumerable<UserForViewDTO>> GetAllAsync(Expression<Func<User, bool>> expression = null);
        ValueTask<UserForViewDTO> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<bool> CreateAsync(UserForCreationDTO dto);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<UserForViewDTO> UpdateAsync(int id, UserForUpdateDTO dto);
        ValueTask<bool> ChangePasswordAsync(UserForChangePasswordDTO dto);
        ValueTask<bool> ChangeRoleAsync(long userId, byte roleId);
    }
}
