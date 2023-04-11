using OnlineStoreManagementSystem.Data.IRepositories;
using OnlineStoreManagementSystem.Domain.Entitties.Attachments;
using OnlineStoreManagementSystem.Service.DTOs.Attachments;
using OnlineStoreManagementSystem.Service.Exceptions;
using OnlineStoreManagementSystem.Service.Helpers;
using OnlineStoreManagementSystem.Service.Interfaces.Attachments;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IGenericRepository<Attachment> attachmentRepository;

        public AttachmentService(IGenericRepository<Attachment> attachmentRepository)
        {
            this.attachmentRepository = attachmentRepository;
        }

        public async ValueTask<Attachment> CreateAsync(string filePath)
        {

            var file = new Attachment()
            {
                Path = filePath
            };

            file = await attachmentRepository.CreateAsync(file);
            await attachmentRepository.SaveChangesAsync();

            return file;
        }

        public async ValueTask<Attachment> UpdateAsync(long id, Stream stream)
        {
            var existAttachment = await attachmentRepository.GetAsync(a => a.Id == id);

            if (existAttachment is null)
                throw new HttpStatusCodeException(404, "Attachment not found.");

            string fileName = existAttachment.Path;
            string filePath = Path.Combine(EnvironmentHelper.WebRootPath, fileName);

            // copy image to the destination as stream
            FileStream fileStream = File.OpenWrite(filePath);
            await stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            await attachmentRepository.SaveChangesAsync();

            return existAttachment;
        }

        public async ValueTask<Attachment> UploadAsync(AttachmentForCreationDTO dto)
        {
            string fileName = Guid.NewGuid().ToString("N") + ".png";
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

            if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);

            // copy image to the destination as stream
            FileStream fileStream = File.OpenWrite(filePath);
            await dto.Stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            return await CreateAsync(Path.Combine(EnvironmentHelper.FilePath, fileName));
        }
    }
}
