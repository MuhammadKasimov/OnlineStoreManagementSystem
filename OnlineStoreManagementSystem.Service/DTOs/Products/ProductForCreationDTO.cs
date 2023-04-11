using Microsoft.AspNetCore.Http;
using OnlineStoreManagementSystem.Service.DTOs.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.DTOs.Products
{
    public class ProductForCreationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AttachemtId { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
