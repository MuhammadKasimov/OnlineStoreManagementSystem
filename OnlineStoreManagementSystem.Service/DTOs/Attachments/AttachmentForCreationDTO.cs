using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.DTOs.Attachments
{
    public class AttachmentForCreationDTO
    {
        [Required]
        public string Path { get; set; }

        [Required]
        public Stream Stream { get; set; }
    }
}
