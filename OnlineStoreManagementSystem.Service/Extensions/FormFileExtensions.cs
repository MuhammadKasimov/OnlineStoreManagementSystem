﻿using Microsoft.AspNetCore.Http;
using OnlineStoreManagementSystem.Service.DTOs.Attachments;
using System.IO;

namespace OnlineStoreManagementSystem.Service.Extensions
{
    public static class FormFileExtensions
    {
        public static AttachmentForCreationDTO ToAttachmentOrDefault(this IFormFile formFile)
        {

            if (formFile?.Length > 0)
            {
                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return new AttachmentForCreationDTO()
                {
                    Path = formFile.FileName,
                    Stream = new MemoryStream(fileBytes)
                };
            }

            return null;
        }
    }
}
