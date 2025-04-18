using BloggingApplicationAPI.Data;
using BloggingApplicationAPI.Models;
using BloggingApplicationAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BloggingApplicationAPI.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            post.CreatedAt = DateTime.UtcNow;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> UpdatePostAsync(int id, Post post)
        {
            var existingPost = await _context.Posts.FindAsync(id);
            if (existingPost == null)
                return null;

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            await _context.SaveChangesAsync();

            return existingPost;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return false;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
