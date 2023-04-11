using OnlineStoreManagementSystem.Domain.Entitties.Products;

namespace OnlineStoreManagementSystem.Domain.Entitties.Users
{
    public class UserProduct
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
