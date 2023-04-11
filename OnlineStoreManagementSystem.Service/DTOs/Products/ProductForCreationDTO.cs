using Microsoft.AspNetCore.Http;

namespace OnlineStoreManagementSystem.Service.DTOs.Products
{
    public class ProductForCreationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
