using OnlineStoreManagementSystem.Domain.Commons;
using OnlineStoreManagementSystem.Domain.Entitties.Carts;
using OnlineStoreManagementSystem.Domain.Entitties.Products;

namespace OnlineStoreManagementSystem.Domain.Entitties.Users
{
    public class UserProduct : Auditable
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public byte NumberOfProducts { get; set; }
        public long CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
