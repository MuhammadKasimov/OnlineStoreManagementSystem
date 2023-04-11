﻿using OnlineStoreManagementSystem.Service.DTOs.Attachments;

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
