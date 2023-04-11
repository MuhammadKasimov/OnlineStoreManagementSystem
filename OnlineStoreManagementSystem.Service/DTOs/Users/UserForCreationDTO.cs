using Microsoft.AspNetCore.Http;

namespace OnlineStoreManagementSystem.Service.DTOs.Users
{
    public class UserForCreationDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
