using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.Interfaces.Users
{
    public interface IAuthService
    {
        ValueTask<string> GenerateToken(string email, string password);
    }
}
