using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(ApiUser user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
