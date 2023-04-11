using OnlineStoreManagementSystem.Domain.Entitties.Attachments;
using OnlineStoreManagementSystem.Service.DTOs.Attachments;
using System.IO;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.Interfaces.Attachments
{
    public interface IAttachmentService
    {
        ValueTask<Attachment> UploadAsync(AttachmentForCreationDTO dto);
        ValueTask<Attachment> UpdateAsync(long id, Stream stream);
        ValueTask<Attachment> CreateAsync(string filePath);
    }
}
