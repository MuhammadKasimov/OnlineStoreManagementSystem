using OnlineStoreManagementSystem.Domain.Commons;
using OnlineStoreManagementSystem.Domain.Entitties.Users;
using System.Collections.Generic;

namespace OnlineStoreManagementSystem.Domain.Entitties.Carts
{
    public class Cart : Auditable
    {
        public decimal TotalPrice { get; set; } = 0;
        public ICollection<UserProduct> Products { get; set; }
    }
}
