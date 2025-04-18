using BloggingApplicationAPI.Models;


namespace BloggingApplicationAPI.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string username, string email, string password);
        Task<User> LoginAsync(string username, string password);
    }
}