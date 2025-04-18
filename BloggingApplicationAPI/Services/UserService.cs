using BloggingApplicationAPI.Data;
using BloggingApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingApplicationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Register a new user
        public async Task<User> RegisterAsync(string username, string email, string password)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (existingUser != null)
                throw new System.Exception("Username already exists.");

            var user = new User
            {
                Username = username,
                Email = email,
                Password = password // Hash password in production (consider bcrypt, etc.)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Login user
        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null)
                throw new System.Exception("Invalid username or password.");

            return user;
        }
    }
}
