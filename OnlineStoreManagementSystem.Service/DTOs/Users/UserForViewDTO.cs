using OnlineStoreManagementSystem.Domain.Entitties.Attachments;

namespace OnlineStoreManagementSystem.Service.DTOs.Users
{
    public class UserForViewDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
