using OnlineStoreManagementSystem.Domain.Entitties.Products;
using OnlineStoreManagementSystem.Domain.Entitties.Users;

namespace OnlineStoreManagementSystem.Service.DTOs.Users
{
    public class UserProductForCreationDTO
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
