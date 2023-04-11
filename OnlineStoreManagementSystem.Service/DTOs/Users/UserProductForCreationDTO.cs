using OnlineStoreManagementSystem.Domain.Entitties.Products;
using OnlineStoreManagementSystem.Domain.Entitties.Users;

namespace OnlineStoreManagementSystem.Service.DTOs.Users
{
    public class UserProductForCreationDTO
    {
        public int ProductId { get; set; }
        public byte NumberOfProducts { get; set; }
        public int CartId { get; set; }
    }
}
