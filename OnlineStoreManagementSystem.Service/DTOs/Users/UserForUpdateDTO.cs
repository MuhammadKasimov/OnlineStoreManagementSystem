using OnlineStoreManagementSystem.Service.DTOs.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.DTOs.Users
{
    public class UserForUpdateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? AttachmentId { get; set; }
        public AttachmentForCreationDTO Attachment { get; set; }
    }
}
