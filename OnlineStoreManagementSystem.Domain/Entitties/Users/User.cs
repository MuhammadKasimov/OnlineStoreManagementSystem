using OnlineStoreManagementSystem.Domain.Commons;
using OnlineStoreManagementSystem.Domain.Entitties.Attachments;
using OnlineStoreManagementSystem.Domain.Enums;
using System.Collections.Generic;

namespace OnlineStoreManagementSystem.Domain.Entitties.Users
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
        public UserRole Role { get; set; }
    }
}
