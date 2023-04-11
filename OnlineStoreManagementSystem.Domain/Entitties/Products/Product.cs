using OnlineStoreManagementSystem.Domain.Commons;
using OnlineStoreManagementSystem.Domain.Entitties.Attachments;

namespace OnlineStoreManagementSystem.Domain.Entitties.Products
{
    public class Product : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
