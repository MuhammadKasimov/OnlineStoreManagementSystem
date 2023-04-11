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
    public interface IUserProductService
    {
        ValueTask<IEnumerable<UserProduct>> GetAllAsync(Expression<Func<UserProduct, bool>> expression = null);
        ValueTask<UserProduct> GetAsync(Expression<Func<UserProduct, bool>> expression);
        ValueTask<UserProduct> CreateAsync(UserProductForCreationDTO dto);
        ValueTask<bool> DeleteAsync(int id);
    }
}
