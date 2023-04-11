using OnlineStoreManagementSystem.Domain.Commons;
using OnlineStoreManagementSystem.Domain.Entitties.Carts;
using OnlineStoreManagementSystem.Domain.Entitties.Products;

namespace OnlineStoreManagementSystem.Domain.Entitties.Users
{
    public class UserProduct : Auditable
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
