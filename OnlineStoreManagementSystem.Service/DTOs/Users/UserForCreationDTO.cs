using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreManagementSystem.Service.DTOs.Users
{
    public class UserForCreationDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
