using OnlineStoreManagementSystem.Domain.Commons;
using OnlineStoreManagementSystem.Domain.Entitties.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Domain.Entitties.Carts
{
    public class Cart : Auditable
    {
        public decimal TotalPrice { get; set; } = 0;
        public ICollection<UserProduct> Products { get; set; }
    }
}
